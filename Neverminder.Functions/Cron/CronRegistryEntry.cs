using NCrontab;

namespace Neverminder.Functions.Cron
{
    public sealed record CronRegistryEntry(Type Type, CrontabSchedule CrontabSchedule);
}
