using EduApi.Application.Models.Requests.Student;
using EduApi.Application.Models.Requests.Subject;
using EduApi.Domain.Entities;

namespace EduApi.Application.Mappers
{
    public static class StudentMapper
    {
        public static StudentResponse ToDto(this Student student)
        {
            return new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                BirthDate = student.BirthDate,
                Gender = student.Gender,
                CurrentGradeLevel = student.CurrentGradeLevel,
                CityName = student.City?.Name,
                CreatedDate = student.CreatedDate,
                LastUpdatedDate = student.LastUpdatedDate,
                DepartmentName = student.Department?.Name,
                ZodiacSign = student.ZodiacSign,
                Subjects = student.StudentSubjects
                    .Where(ss => ss.Subject != null)
                    .Select(ss => new StudentSubjectResponse
                    {
                        Id = ss.SubjectId,
                        Name = ss.Subject.Name ?? string.Empty,
                        Mark = ss.Mark
                    }).ToList() ?? new List<StudentSubjectResponse>() 
            };
        }

        public static Student ToEntity(this CreateStudentRequest createStudentRequest)
        {
            return new Student
            {
                Name = createStudentRequest.Name!,
                BirthDate = createStudentRequest.BirthDate,
                Gender = createStudentRequest.Gender,
                CurrentGradeLevel = createStudentRequest.CurrentGradeLevel,
                CityId = createStudentRequest.CityId,
                DepartmentId = createStudentRequest.DepartmentId,
                ZodiacSign = createStudentRequest.ZodiacSign,
                StudentSubjects = createStudentRequest.SubjectsId.Select(subjectId => new StudentSubject
                {
                    SubjectId = subjectId,
                }).ToList()
            };
        }
    }
}
