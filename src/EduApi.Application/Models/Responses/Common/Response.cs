namespace EduApi.Application.Models.Requests.Common;

public class Response<T> : Response
{
    public T? Result { get; set; }

    public static implicit operator Response<T>(T result)
    {
        return new Response<T>
        {
            Result = result,
            Error = null
        };
    }

    public static implicit operator Response<T>(ResponseError error)
    {
        return new Response<T>
        {
            Result = default,
            Error = error
        };
    }
}

public class Response
{
    public bool IsSuccess => Error == null;
    public ResponseError? Error { get; set; }

    public Response(ResponseError error)
    {
        Error = error;
    }

    public Response()
    {

    }


    public static implicit operator Response(ResponseError error)
    {
        return new Response
        {
            Error = error
        };
    }

    public static implicit operator Response(bool success)
    {
        return new Response
        {
            Error = null
        };
    }
}