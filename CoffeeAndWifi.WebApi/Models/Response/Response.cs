namespace CoffeeAndWifi.WebApi.Models.Response
{
    public class Response
    {
        public Response()
        {
            Errors = new List<Error>();
        }

        public bool IsSuccess => Errors == null || !Errors.Any();

        public List<Error> Errors { get; set; }
        public string Jwt { get; set; } = null!;

    }
}
