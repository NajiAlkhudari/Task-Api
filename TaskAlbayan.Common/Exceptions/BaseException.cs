using TaskAlbayan.Utils;

namespace TaskAlbayan.Common.Exceptions
{
    public class BaseException : Exception
    {
        public bool Success  => false;
        public int StatusCode {get;}
        public object? Value {get;}

        public BaseException(string message , int statusCode , ErrorResponse value):base(message)=>
        (StatusCode , Value)=(statusCode , value);

    }
}