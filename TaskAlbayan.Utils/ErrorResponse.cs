namespace TaskAlbayan.Utils
{
       public class ErrorResponse
    {
        public ErrorResponse(string message, int statusCode, object errors)
        {
            Message = message;
            StatusCode = statusCode;
            Errors = errors;
        }
        public bool Success => false;
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }


    }

    
}