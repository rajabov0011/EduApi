using EduApi.Domain.Enums;

namespace EduApi.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public long CityId { get; set; }
        public City City { get; set; } = null!;
        public ICollection<Subject> Subjects{ get; set; } = [];
    }
}
