
namespace OkalaExchange.Contracts
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "successfully Done.";
        public T Data { get; set; }
        public ErrorDetails Error { get; set; }


    }
}
