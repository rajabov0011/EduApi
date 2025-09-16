namespace EduApi.Domain.Entities
{
    public class TeacherSubject : BaseEntity
    {
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }

        public Teacher Teacher { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
    }
}
