using EduApi.Application.Interfaces;
using EduApi.Application.Mappers;
using EduApi.Application.Models.Requests.Teacher;
using EduApi.Application.Models.Requests.Common;
using EduApi.Domain.Entities;
using EduApi.Infrastructure.Data;
using EduApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduApi.Infrastructure.Services
{
    public class TeacherService(EduApiContext dbContext) : ITeacherService
    {
        public async Task<Response<TeacherResponse>> CreateTeacherAsync(CreateTeacherRequest createTeacherDto)
        {
            var subjects = await dbContext.Subjects
                .Where(s => createTeacherDto.Subjects.Contains(s.Id))
                .ToListAsync();

            var teacher = createTeacherDto.ToEntity();
            teacher.Subjects = subjects;

            await dbContext.Teachers.AddAsync(teacher);
            await dbContext.SaveChangesAsync();

            return teacher.ToDto();
        }

        public async Task<Response<ListResponse<TeacherResponse>>> GetAllTeachersAsync(TeacherFilterRequest filter)
        {
            var query = dbContext.Teachers
                .Include(t => t.City)
                .Include(t => t.Subjects)
                .AsQueryable();

            if (filter.Gender.HasValue)
                query = query.Where(t => t.Gender == filter.Gender.Value);

            if (filter.Age.HasValue)
                query = query.Where(t => (DateTime.UtcNow.Year - t.BirthDate.Year) >= filter.Age.Value);

            if (!string.IsNullOrWhiteSpace(filter.Subject))
                query = query.Where(t => t.Subjects.Any(s => EF.Functions.ILike(s.Name, filter.Subject)));

            var totalCount = await query.CountAsync();

            var pageNumber = filter.PageNumber;
            var pageSize = filter.PageSize;

            query = query.ToPaged(pageNumber, pageSize);

            var teachers = await query.Select(t => t.ToDto()).ToListAsync();

            var pagedList = new ListResponse<TeacherResponse>(totalCount, pageNumber, pageSize, teachers);

            return pagedList;
        }

        public async Task<Response<List<TeacherResponse>>> GetTopTeachersByStudentPerformanceAsync(TopTeachersByStudentPerformanceRequest request)
        {
            var subjectName = request.Subject;
            var getLowestMarks = request.WithLowestMarks;
            var teachersCount = request.TeachersCount;
            var topStudentsCount = request.TopStudentsCount;

            var subject = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Name == subjectName && !s.IsDeleted);
            var subjectId = subject?.Id;

            var studentMarksQuery = dbContext.StudentSubjects
                .Include(ss => ss.Student)
                .Where(ss => ss.SubjectId == subjectId && !ss.IsDeleted && !ss.Student.IsDeleted);

            studentMarksQuery = getLowestMarks
                ? studentMarksQuery.OrderBy(ss => ss.Mark).ThenBy(ss => ss.Student.CurrentGradeLevel)
                : studentMarksQuery.OrderByDescending(ss => ss.Mark).ThenBy(ss => ss.Student.CurrentGradeLevel);
            
            var topStudentIds = await studentMarksQuery
                .Select(ss => ss.StudentId)
                .Distinct()
                .Take(topStudentsCount)
                .ToListAsync();

            var teacherIds = await dbContext.Teachers
                .Where(t => t.Subjects.Any(s => s.Id == subjectId))
                .Select(t => t.Id)
                .Distinct()
                .ToListAsync();


            var topTeachers = await dbContext.Teachers
                .Where(t => teacherIds.Contains(t.Id) && !t.IsDeleted)
                .Take(teachersCount)
                .Select(t => t.ToDto())
                .ToListAsync();

            return topTeachers;
        }

        public async Task<Response<TeacherResponse>> GetTeacherByIdAsync(long id)
        {
            var teacher = await dbContext.Teachers
                .Where(t => !t.IsDeleted)
                .Include(t => t.City)
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                return new ResponseError(HttpStatusCode.NotFound, "Teacher not found.");

            return teacher.ToDto();
        }

        public async Task<Response<TeacherResponse>> UpdateTeacherAsync(UpdateTeacherRequest updateTeacherRequest)
        {
            var teacher = await dbContext.Teachers
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == updateTeacherRequest.Id);

            if (teacher == null)
                return new ResponseError(HttpStatusCode.NotFound, "Teacher not found.");


            if (!string.IsNullOrWhiteSpace(updateTeacherRequest.Name) && teacher.Name != updateTeacherRequest.Name)
                teacher.Name = updateTeacherRequest.Name;
            
            if (updateTeacherRequest.CityId.HasValue && teacher.CityId != updateTeacherRequest.CityId)
                teacher.CityId = updateTeacherRequest.CityId.Value;

            if (updateTeacherRequest.Subjects.Count > 0 && teacher.Subjects.Count > 0)
            {
                var subjects = await dbContext.Subjects.Where(s => updateTeacherRequest.Subjects.Contains(s.Id)).ToListAsync();

                foreach (var subject in subjects)
                {
                    var subjectExist = teacher.Subjects.Any(s => s.Id == subject.Id);
                    if(!subjectExist)
                        teacher.Subjects.Add(subject);
                }
            }

            teacher.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return teacher.ToDto();
        }

        public async Task<Response> DeleteTeacherAsync(long id)
        {
            var teacher = await dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
            if (teacher == null)
                return new ResponseError(HttpStatusCode.NotFound, "Teacher not found.");

            teacher.IsDeleted = true;
            teacher.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
