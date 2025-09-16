using EduApi.Domain.Enums;

namespace EduApi.Application.Models.Requests.Teacher
{
    public class CreateTeacherRequest
    {
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public long CityId { get; set; }
        public IList<long> Subjects { get; set; } = [];
    }
}
