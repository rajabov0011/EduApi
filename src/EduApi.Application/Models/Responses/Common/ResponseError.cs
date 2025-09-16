using System.Net;

namespace EduApi.Application.Models.Requests.Common;
public class ResponseError
{
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }

    public ResponseError(int code, string message)
    {
        Message = message;
        Code = code;
    }

    public ResponseError(HttpStatusCode statusCode, string message)
    {
        Message = message;
        Code = (int)statusCode;
    }
}