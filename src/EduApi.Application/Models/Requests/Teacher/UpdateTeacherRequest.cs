namespace EduApi.Application.Models.Requests.Teacher
{
    public class UpdateTeacherRequest
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? CityId { get; set; }
        public IList<long> Subjects { get; set; } = [];
    }
}
