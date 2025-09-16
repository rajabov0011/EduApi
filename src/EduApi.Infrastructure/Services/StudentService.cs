using EduApi.Application.Interfaces;
using EduApi.Application.Mappers;
using EduApi.Application.Models.Requests.Student;
using EduApi.Application.Models.Requests.Common;
using EduApi.Domain.Entities;
using EduApi.Infrastructure.Data;
using EduApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduApi.Infrastructure.Services
{
    public class StudentService(EduApiContext dbContext) : IStudentService
    {
        public async Task<Response<StudentResponse>> CreateStudentAsync(CreateStudentRequest createStudentRequest)
        {
            if (createStudentRequest == null)
                return new ResponseError((int)HttpStatusCode.NotFound, "Student not found");

            createStudentRequest.ZodiacSign = Zodiac.GetZodiacSign(createStudentRequest.BirthDate);
            var student = createStudentRequest.ToEntity();
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();

            await dbContext.Entry(student).Reference(s => s.City).LoadAsync();
            await dbContext.Entry(student).Reference(s => s.Department).LoadAsync();

            return student.ToDto();
        }

        public async Task<Response<ListResponse<StudentResponse>>> GetAllStudentsAsync(StudentFilterRequest filter)
        {
            var studentsQuery = GetFilteredStudents(filter);

            var totalCount = await studentsQuery.CountAsync();

            var pageNumber = filter.PageNumber;
            var pageSize = filter.PageSize;

            var students = await studentsQuery.ToPaged(pageNumber, pageSize).Select(s => s.ToDto()).ToListAsync();

            var result = new ListResponse<StudentResponse>(totalCount, pageNumber, pageSize, students);

            return result;
        }

        public async Task<Response<StudentResponse>> GetStudentByIdAsync(long id)
        {
            var student = await dbContext.Students
                .Where(s => !s.IsDeleted)
                .Include(s => s.City)
                .Include(s => s.Department)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(s => s.Id == id);

            if(student == null)
                return new ResponseError((int)HttpStatusCode.NotFound, "Student not found");

            return student.ToDto();
        }

        public async Task<Response<StudentResponse>> UpdateStudentAsync(UpdateStudentRequest updateStudentRequest)
        {
            var student = await dbContext.Students
                .Include(s => s.StudentSubjects)
                .FirstOrDefaultAsync(s => s.Id == updateStudentRequest.Id);

            if (student == null)
                return new ResponseError((int)HttpStatusCode.NotFound, "Student not found");

            if (!string.IsNullOrWhiteSpace(updateStudentRequest.Name) && student.Name != updateStudentRequest.Name)
                student.Name = updateStudentRequest.Name;
            
            if (updateStudentRequest.CityId.HasValue && student.CityId != updateStudentRequest.CityId)
                student.CityId = updateStudentRequest.CityId.Value;

            if (updateStudentRequest.DepartmentId.HasValue && student.DepartmentId != updateStudentRequest.DepartmentId)
                student.DepartmentId = updateStudentRequest.DepartmentId.Value;

            if (updateStudentRequest.GradeLevel.HasValue && student.CurrentGradeLevel != updateStudentRequest.GradeLevel)
                student.CurrentGradeLevel = updateStudentRequest.GradeLevel.Value;

            if (updateStudentRequest.Subjects.Count > 0 && student.StudentSubjects.Count > 0)
            {
                foreach(var subject in updateStudentRequest.Subjects)
                {
                    var studentSubject = student.StudentSubjects.FirstOrDefault(ss => ss.SubjectId == subject.SubjectId);
                    if(studentSubject == null)
                    {
                        studentSubject = new StudentSubject
                        {
                            StudentId = updateStudentRequest.Id,
                            SubjectId = subject.SubjectId,
                            CreatedDate = DateTime.UtcNow,
                        };

                        await dbContext.StudentSubjects.AddAsync(studentSubject);
                    }

                    studentSubject.Mark = subject.Mark;
                }
            }
            
            student.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return student.ToDto();
        }

        public async Task<Response> DeleteStudentAsync(long id)
        {
            var student = await dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return new ResponseError((int)HttpStatusCode.NotFound, "Student not found");

            student.IsDeleted = true;

            await dbContext.SaveChangesAsync();

            return new Response();
        }

        private IQueryable<Student> GetFilteredStudents(StudentFilterRequest filter)
        {
            var query = dbContext.Students
                .Include(s => s.City)
                .Include(s => s.Department)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .Where(s => !s.IsDeleted)
                .AsQueryable();

            if (filter.Gender.HasValue)
                query = query.Where(s => s.Gender == filter.Gender.Value);

            if (!string.IsNullOrWhiteSpace(filter.Department))
                query = query.Where(s => EF.Functions.ILike(s.Department.Name, filter.Department));

            if (!string.IsNullOrWhiteSpace(filter.City))
                query = query.Where(s => EF.Functions.ILike(s.City.Name, filter.City));

            if (filter.GradeLevel.HasValue)
                query = query.Where(s => s.CurrentGradeLevel == filter.GradeLevel.Value);

            if (filter.Age.HasValue)
                query = query.Where(s => (DateTime.UtcNow.Year - s.BirthDate.Year) >= filter.Age.Value);

            if (!string.IsNullOrWhiteSpace(filter.Subject))
            {
                query = query.Where(s => s.StudentSubjects.Any(ss => EF.Functions.ILike(ss.Subject.Name, filter.Subject)));
            }

            if (filter.ZodiacSign.HasValue)
            {
                query = query
                    .Where(s => s.ZodiacSign == filter.ZodiacSign.Value);
            }

            return query;
        }

        private async Task<IQueryable<Student>> GetTopStudentsBySubjectAsync(TopStudentsFilterRequest filterRequest)
        {
            var subject = await dbContext.Subjects.FirstOrDefaultAsync(s => EF.Functions.ILike(s.Name, filterRequest.Subject ?? string.Empty) && !s.IsDeleted);

            var subjectId = subject?.Id ?? 0;

            var students = dbContext.StudentSubjects
                .Where(ss => ss.SubjectId == subjectId)
                .Include(ss => ss.Student)
                    .ThenInclude(s => s.City)
                .Include(ss => ss.Student)
                    .ThenInclude(s => s.Department)
                .OrderByDescending(ss => ss.Mark)
                .ThenBy(ss => ss.Student.CurrentGradeLevel)
                .ThenBy(ss => ss.Student.BirthDate)
                .Select(ss => ss.Student)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                    .Take(filterRequest.Count);

            return students;
        }

        public async Task<Response<List<StudentResponse>>> GetTopStudentsAsync(TopStudentsFilterRequest topStudentsFilterRequest)
        {
            var topStudentsQuery = await GetTopStudentsBySubjectAsync(topStudentsFilterRequest);

            var students = await topStudentsQuery.Select(s => s.ToDto()).ToListAsync();

            return students;
        }
    }
}
