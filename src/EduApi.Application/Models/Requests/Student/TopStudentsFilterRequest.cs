using EduApi.Application.Models.Requests.Common;

namespace EduApi.Application.Models.Requests.Student;
public class TopStudentsFilterRequest
{
    public string? Subject { get; set; }
    public int Count { get; set; } = 10;
}
