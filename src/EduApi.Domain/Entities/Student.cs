using EduApi.Domain.Enums;

namespace EduApi.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int CurrentGradeLevel { get; set; }
        public long CityId { get; set; }
        public City City { get; set; } = null!;
        public long DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public ZodiacSign ZodiacSign { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
