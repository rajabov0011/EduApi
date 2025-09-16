namespace EduApi.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = [];
    }
}
