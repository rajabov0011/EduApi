using EduApi.Application.Models.Requests.Common;

namespace EduApi.Application.Models.Requests.Subject
{
    public class SubjectFilterRequest : PaginationFilterRequest
    {
        public string? Name { get; set; }
    }
}
