using EduApi.Application.Models.Requests.Subject;
using EduApi.Domain.Enums;

namespace EduApi.Application.Models.Requests.Teacher
{
    public class TeacherResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? CityName { get; set; }
        public IList<SubjectResponse> Subjects { get; set; } = [];
    }
}
