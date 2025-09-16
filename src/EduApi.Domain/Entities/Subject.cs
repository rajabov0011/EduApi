namespace EduApi.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int? GradeLevel { get; set; }
        public ICollection<Teacher> Teachers { get; set; } = [];
    }
}
