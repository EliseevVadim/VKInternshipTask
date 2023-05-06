using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
