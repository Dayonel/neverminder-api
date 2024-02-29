namespace Neverminder.Functions.Interfaces
{
    public interface ICronJob
    {
        Task Run(CancellationToken token = default);
    }
}
