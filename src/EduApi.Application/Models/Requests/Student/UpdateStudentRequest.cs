namespace EduApi.Application.Models.Requests.Student
{
    public class UpdateStudentRequest
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? CityId { get; set; }
        public long? DepartmentId { get; set; }
        public int? GradeLevel { get; set; }
        public IList<StudentSubjectMarkRequest> Subjects { get; set; } = [];
    }
}
