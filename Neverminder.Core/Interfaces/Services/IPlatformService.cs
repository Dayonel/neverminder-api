namespace Neverminder.Core.Interfaces.Services
{
    public interface IPlatformService
    {
        Task<bool> Post(string pushToken);
        Task<bool> Send(string pushToken);
    }
}
