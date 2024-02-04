namespace Ciizo.CleanPattern.Domain.Core.Models
{
    public sealed class Result<TValue>
    {
        public TValue? Value { get; init; }
        public Exception? Error { get; init; }
        public bool IsError { get; init; }
        public bool IsSuccess => !IsError;

        private Result(TValue value)
        {
            IsError = false;
            Value = value;
            Error = null;
        }

        private Result(Exception error)
        {
            IsError = true;
            Value = default;
            Error = error;
        }

        public static implicit operator Result<TValue>(TValue v) => new(v);
        public static implicit operator Result<TValue>(Exception e) => new(e);

        public TResult Match<TResult>(
            Func<TValue, TResult> success,
            Func<Exception, TResult> failure) => IsSuccess ? success(Value!) : failure(Error!);
    }
}