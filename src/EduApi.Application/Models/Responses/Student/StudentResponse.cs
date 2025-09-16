using EduApi.Application.Models.Requests.Subject;
using EduApi.Domain.Enums;

namespace EduApi.Application.Models.Requests.Student
{
    public class StudentResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int? CurrentGradeLevel { get; set; }
        public string? CityName { get; set; }
        public string? DepartmentName { get; set; }
        public ZodiacSign ZodiacSign { get; set; }
        public IList<StudentSubjectResponse> Subjects { get; set; } = [];
        public DateTime LastUpdatedDate { get; internal set; }
        public DateTime CreatedDate { get; internal set; }
    }
}
