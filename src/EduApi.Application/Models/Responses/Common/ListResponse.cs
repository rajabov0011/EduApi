namespace EduApi.Application.Models.Requests.Common
{
    public class ListResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = [];

        public ListResponse(int totalCount, int pageNumber, int pageSize, List<T> items)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }
    }
}
