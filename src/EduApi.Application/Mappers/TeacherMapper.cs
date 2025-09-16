using EduApi.Application.Models.Requests.Teacher;
using EduApi.Application.Models.Requests.Teacher;
using EduApi.Domain.Entities;

namespace EduApi.Application.Mappers
{
    public static class TeacherMapper
    {
        public static TeacherResponse ToDto(this Teacher teacher)
        {
            return new TeacherResponse
            {
                Id = teacher.Id,
                Name = teacher.Name,
                CityName = teacher.City?.Name,
                BirthDate = teacher.BirthDate,
                Gender = teacher.Gender,
                Subjects = teacher.Subjects.Select(s => s.ToDto()).ToList()
            };
        }

        public static Teacher ToEntity(this CreateTeacherRequest dto)
        {
            return new Teacher
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                CityId = dto.CityId,
                Subjects = dto.Subjects?
                    .Select(id => new Subject { Id = id }).ToList() ?? new List<Subject>()
            };
        }
    }
}
