namespace Vezeeta.ResponseShape
{
    public class serviceResponse<T>:BaseResponse
    {
        public T data { get; set; }
    }
}
