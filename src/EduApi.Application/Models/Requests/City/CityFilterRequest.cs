using EduApi.Application.Models.Requests.Common;

namespace EduApi.Application.Models.Requests.City
{
    public class CityFilterRequest : PaginationFilterRequest
    {
        public string? Name { get; set; }
    }
}
