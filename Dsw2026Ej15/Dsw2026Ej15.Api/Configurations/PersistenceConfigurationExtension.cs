using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api.Configurations
{
    public static class PersistenceConfigurationExtension
    {
        public static IServiceCollection AddAplicationPersistence(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Dsw2026Ej15DbContext>(options =>
            {
                options.UseSqlServer(conectionString);
            });
            return services;
        }
        
        public static IHost LoadSpecialityData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<Dsw2026Ej15DbContext>();
            context.SeedworkSpecialities(@"specialities.json");
            return host;

        }

    }
}
