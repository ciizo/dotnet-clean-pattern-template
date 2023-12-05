namespace Ciizo.Restful.Onion.Domain.Business.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string dataName) : base($"{dataName} not found.")
        {
        }
    }
}
