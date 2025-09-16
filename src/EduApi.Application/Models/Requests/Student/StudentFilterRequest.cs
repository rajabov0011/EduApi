using EduApi.Application.Models.Requests.Common;
using EduApi.Domain.Enums;

namespace EduApi.Application.Models.Requests.Student
{
    public class StudentFilterRequest : PaginationFilterRequest
    {
        public Gender? Gender { get; set; }
        public string? Department { get; set; }
        public string? City { get; set; }
        public int? GradeLevel { get; set; }
        public ZodiacSign? ZodiacSign { get; set; }
        public int? Age { get; set; }
        public string? Subject { get; set; }
    }
}
