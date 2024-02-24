namespace Neverminder.Functions.Interfaces
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
    }
}
