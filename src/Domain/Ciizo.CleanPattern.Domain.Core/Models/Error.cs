namespace Ciizo.CleanPattern.Domain.Core.Models
{
    public sealed class Error : Exception
    {
        public string Code { get; init; } = string.Empty;

        public Error(string message)
            : base(message)
        {
        }

        public Error(string code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}