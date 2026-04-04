namespace Application.Common.Responses;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public static Response<T> Success(T data, string message = "Operation completed successfully")
    {
        return new Response<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

    public static Response<T> Failure(string message, List<string>? errors = null)
    {
        return new Response<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}