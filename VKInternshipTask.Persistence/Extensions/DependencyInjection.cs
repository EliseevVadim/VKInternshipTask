using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VKInternshipTask.Application.Common.Interfaces;

namespace VKInternshipTask.Persistence.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration["ConnectionString"];
            services.AddDbContext<UsersAPIDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IUsersAPIDbContext>(provider => provider.GetRequiredService<UsersAPIDbContext>());
            return services;
        }
    }
}
