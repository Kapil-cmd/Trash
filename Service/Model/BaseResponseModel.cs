
namespace Application
{
    public class BaseResponseModel<T>
    {
     public BaseResponseModel(string status, string message, T response = default(T),List<Error> erros = null)
        {
            Status = status;
            Message = message;
            Data = response;
            Errors = erros;
        }
        public BaseResponseModel()
        {
        }
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<Error> Errors { get; set; }
    }
    public class Error
    {
        public string ErrorField { get; set; }
        public string ErrorMessage { get; set; }
    }
}
