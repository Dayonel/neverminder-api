using Neverminder.Functions.Interfaces;

namespace Neverminder.Functions.Cron
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
    }
}
