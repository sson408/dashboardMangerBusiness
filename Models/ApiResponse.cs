namespace dashboardManger.Models
{
    public class PageInfo
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }

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

    public class PagedApiResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public PageInfo PageInfo { get; set; }
        public List<T> Data { get; set; }

        public PagedApiResponse(int code, string msg, PageInfo pageInfo, List<T> data)
        {
            Code = code;
            Message = msg;
            PageInfo = pageInfo;
            Data = data;
        }
    }
}
