namespace UnitConverter.API.DTOs
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string? message = null)
        {
           return new() { 
               IsSuccess = true, 
               Data = data, 
               Message = message 
           };
        }

        public static ApiResponse<T> Failure(string message)
        {
           return new() { 
               IsSuccess = false, 
               Data = default, 
               Message = message 
           };
        }
    }
}
