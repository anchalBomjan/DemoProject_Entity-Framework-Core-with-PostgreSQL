namespace Application.Common.Responses;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }

    public static Response<T> Success(T data) => new() { IsSuccess = true, Data = data };
    public static Response<T> Failure(string message) => new() { IsSuccess = false, Message = message };
}