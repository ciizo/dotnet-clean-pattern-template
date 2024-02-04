namespace Ciizo.CleanPattern.Domain.Core.Models
{
    public sealed class EmptyResult
    {
        public Exception? Error { get; init; }
        public bool IsError { get; init; }
        public bool IsSuccess => !IsError;

        public EmptyResult()
        {
            IsError = false;
            Error = null;
        }

        private EmptyResult(Exception error)
        {
            IsError = true;
            Error = error;
        }

        public static implicit operator EmptyResult(Exception e) => new(e);

        public TResult Match<TResult>(
            Func<TResult> success,
            Func<Exception, TResult> failure) => IsSuccess ? success() : failure(Error!);
    }
}