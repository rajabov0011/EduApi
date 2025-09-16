using EduApi.Application.Models.Requests.Subject;
using EduApi.Domain.Entities;

namespace EduApi.Application.Mappers
{
    public static class SubjectMapper
    {
        public static SubjectResponse ToDto(this Subject subject)
        {
            return new SubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }

        public static Subject ToEntity(this CreateSubjectRequest dto)
        {
            return new Subject
            {
                Name = dto.Name ?? string.Empty
            };
        }
    }
}
