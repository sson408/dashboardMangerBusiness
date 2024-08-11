namespace dashboardManger.Models
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(int code, string msg, T data)
        {
            Code = code;
            Message = msg;
            Data = data;
        }
    }
}
