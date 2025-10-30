
namespace Core
{
    public class SpResponse<T>
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; } 

        public T Data { get; set; }
    }
}
