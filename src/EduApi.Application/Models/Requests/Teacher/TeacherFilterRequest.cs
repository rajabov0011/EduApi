using EduApi.Application.Models.Requests.Common;
using EduApi.Domain.Enums;

namespace EduApi.Application.Models.Requests.Teacher
{
    public class TeacherFilterRequest : PaginationFilterRequest
    {
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public string? City { get; set; }
        public int? Age { get; set; }
        public string? Subject{ get; set; }
    }
}
