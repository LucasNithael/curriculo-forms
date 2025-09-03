namespace backend.Errors
{
    public class ApiException(string statusCode, string message, string detail)
    {
        public string StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public string Detail { get; set; } = detail;
    }
}
