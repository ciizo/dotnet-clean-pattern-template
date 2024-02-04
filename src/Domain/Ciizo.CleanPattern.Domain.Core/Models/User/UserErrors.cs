namespace Ciizo.CleanPattern.Domain.Core.Models.User
{
    public readonly struct UserErrors
    {
        public static readonly Error UserNotFound = new("User.NotFound", "User not found.");

        public static Error NotFound(Guid id) => new("User.NotFound", $"User id {id} not found.");
    }
}
