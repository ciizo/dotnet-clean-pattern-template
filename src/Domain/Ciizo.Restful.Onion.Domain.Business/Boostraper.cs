using Ciizo.Restful.Onion.Domain.Business.User;
using Microsoft.Extensions.DependencyInjection;

namespace Ciizo.Restful.Onion.Domain.Business
{
    public static class Boostraper
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}