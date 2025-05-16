namespace EscolaPro.Extensions
{
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; set; }
        public object? Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<string>? Fields { get; set; }

        public HttpResponseException(int statusCode, string message)
        {
            StatusCode = statusCode;
            Value = new { message };
            TimeStamp = DateTime.UtcNow;
        }

        public HttpResponseException(int statusCode, string message, List<string> field)
        {
            StatusCode = statusCode;
            Value = new { message };
            TimeStamp = DateTime.UtcNow;
            Fields = field;
        }
    }
}
