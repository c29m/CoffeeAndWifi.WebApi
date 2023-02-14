namespace CoffeeAndWifi.WebApi.Models.Response
{
    public class Error
    {
        public Error(string source, string message)
        {
            Source = source;
            Message = message;
        }

        public string Source { get; set; }
        public string Message { get; set; }

    }
}
