namespace EduApi.Application.Models.Requests.Common
{
    public class PaginationFilterRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}