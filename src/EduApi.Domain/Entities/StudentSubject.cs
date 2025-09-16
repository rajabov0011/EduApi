namespace EduApi.Domain.Entities
{
    public class StudentSubject : BaseEntity
    {
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public int Mark { get; set; }

        public Student Student { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
    }
}
