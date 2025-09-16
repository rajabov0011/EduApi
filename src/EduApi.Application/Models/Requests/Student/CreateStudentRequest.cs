using EduApi.Domain.Enums;

namespace EduApi.Application.Models.Requests.Student
{
    public class CreateStudentRequest
    {
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int CurrentGradeLevel { get; set; }
        public long CityId { get; set; }
        public long DepartmentId { get; set; }
        public ZodiacSign ZodiacSign { get; set; }
        public IList<long> SubjectsId { get; set; } = [];
    }
}
