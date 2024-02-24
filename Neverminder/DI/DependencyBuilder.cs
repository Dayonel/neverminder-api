using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Neverminder.Core.Interfaces.Infrastructure;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Core.Interfaces.Services;
using Neverminder.Core.Services;
using Neverminder.Data;
using Neverminder.Data.Repositories;
using Neverminder.Functions.Schedulers;
using Neverminder.Infrastructure.Firebase;
using Neverminder.Functions.Extensions;

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
            services.AddDbContext<NeverminderDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DbContext")));
            #endregion

            #region Services
            services.AddTransient<IPlatformService, PlatformService>();
            services.AddTransient<IReminderService, ReminderService>();
            #endregion

            #region Repositories
            services.AddTransient<IPlatformRepository, PlatformRepository>();
            services.AddTransient<IReminderRepository, ReminderRepository>();
            #endregion

            #region ServiceClients
            FirebaseApp.Create(new AppOptions { Credential = GoogleCredential.FromFile("neverminder-me-firebase-adminsdk-pq10q-ab46b52acc.json") });
            services.AddTransient<IFirebaseServiceClient, FirebaseServiceClient>();
            #endregion

            #region Functions
            services.AddCronJob<PushNotificationScheduler>(c => c.CronExpression = @"* * * * *");
            #endregion
        }
    }
}
