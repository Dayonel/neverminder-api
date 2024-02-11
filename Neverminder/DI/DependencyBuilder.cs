using Microsoft.EntityFrameworkCore;
using Neverminder.Data;

namespace Neverminder.DI
{
    public static class DependencyBuilder
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            #region Settings
            #endregion

            #region DB
            services.AddDbContext<NeverminderDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DbContext")));
            #endregion

            #region Services
            #endregion

            #region ServiceClients
            #endregion

            #region Repositories
            #endregion
        }
    }
}
