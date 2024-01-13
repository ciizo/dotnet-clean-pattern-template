using Ciizo.CleanPattern.Domain.Business.User;
using Microsoft.Extensions.DependencyInjection;

namespace Ciizo.CleanPattern.Domain.Business
{
    public static class Boostraper
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}