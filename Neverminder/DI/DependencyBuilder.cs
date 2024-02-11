using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Neverminder.Core.Interfaces.Infrastructure;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Core.Interfaces.Services;
using Neverminder.Core.Services;
using Neverminder.Data;
using Neverminder.Data.Repositories;
using Neverminder.Infrastructure.Firebase;

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
            services.AddTransient<IPlatformService, PlatformService>();
            #endregion

            #region Repositories
            services.AddTransient<IPlatformRepository, PlatformRepository>();
            #endregion

            #region ServiceClients
            FirebaseApp.Create(new AppOptions { Credential = GoogleCredential.FromFile("neverminder-me-firebase-adminsdk-pq10q-ab46b52acc.json") });
            services.AddTransient<IFirebaseServiceClient, FirebaseServiceClient>();
            #endregion
        }
    }
}
