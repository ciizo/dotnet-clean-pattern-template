using Newtonsoft.Json;

namespace Ciizo.Restful.Onion.Api.Middlewares.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}