using EduApi.Application.Models.Requests.Common;

namespace EduApi.Application.Models.Requests.Department
{
    public class DepartmentFilterRequest : PaginationFilterRequest
    {
        public string? Name { get; set; }
    }
}
