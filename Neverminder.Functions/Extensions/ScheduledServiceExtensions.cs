using Microsoft.Extensions.DependencyInjection;
using Neverminder.Functions.Cron;
using Neverminder.Functions.Interfaces;

namespace Neverminder.Functions.Extensions
{
    public static class ScheduledServiceExtensions
    {
        public static void AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");

            var config = new ScheduleConfig<T>();
            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
        }
    }
}
