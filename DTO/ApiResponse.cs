namespace SharedDTO
{
    public class ApiResponse<T>
    {
        public string CommandMessage { get; set; }
        public bool IsValidReponse { get; set; }
        public T Datalist { get; set; }
        public int TotalCount { get; set; }
        public int Status { get; set; }

        public ApiResponse()
        {
            CommandMessage = "Something went wrong, Try again Later.";
            IsValidReponse = false;
            Datalist = default;
            TotalCount = 0;
            Status = (int)SharedEnums.ApiResponseStatus.BadRequest;
        }
        public ApiResponse(string msg, bool isValidReponse, T data, int totalCount)
        {
            CommandMessage = msg;
            IsValidReponse = isValidReponse;
            Datalist = data;
            TotalCount = totalCount;
        }
        public ApiResponse(string msg, bool isValidReponse, T data)
        {
            CommandMessage = msg;
            IsValidReponse = isValidReponse;
            Datalist = data;
        }
    }
    
}
