namespace TaskAlbayan.Utils
{
    
    public class HttpResponse <T>
    {
        public bool Success {get;set;}=true;
        public T Data {get; private set;}
          public HttpResponse(T data)
        {

            Data = data;

        }

    }
}