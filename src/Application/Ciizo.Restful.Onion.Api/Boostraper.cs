using Ciizo.Restful.Onion.Api.Middlewares;
using Ciizo.Restful.Onion.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ciizo.Restful.Onion.Api
{
    public static class Boostraper
    {
        public static void RegisterMiddlewares(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void InitDatabase(this WebApplication app, IConfiguration configuration)
        {
            if (!configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}