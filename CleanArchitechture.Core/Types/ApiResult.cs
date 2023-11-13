using CleanArchitechture.Core.Enums;

namespace CleanArchitechture.Core.Types;
public class ApiResult<T>
{
    public ApiResult(T data)
    {
        this.Data = data;
    }
    public ApiResult() { }
    public bool IsSuccess { get; set; } = true;
    public T Data { get; set; }
    public string Message { get; set; }
    public MessageDisplayType MessageDisplayType { get; set; }
    public object Error { get; set; }
}