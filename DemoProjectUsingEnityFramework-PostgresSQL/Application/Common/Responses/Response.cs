namespace Application.Common.Responses;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public static Response<T> Success(T data) => new() { IsSuccess = true, Data = data };
    public static Response<T> Success(T data, string message) => new() { IsSuccess = true, Data = data, Message = message };
    public static Response<T> Failure(string message) => new() { IsSuccess = false, Message = message };
    public static Response<T> Failure(string message, List<string> errors) => new() { IsSuccess = false, Message = message, Errors = errors };
}