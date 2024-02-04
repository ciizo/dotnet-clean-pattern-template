using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ciizo.CleanPattern.Domain.Business
{
    public static class Boostraper
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<User.IUserService, User.UserService>();
            services.AddScoped<UserResultPattern.IUserService, UserResultPattern.UserService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}