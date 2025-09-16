namespace EduApi.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = [];
        public ICollection<Teacher> Teachers { get; set; } = [];
    }
}
