namespace Neverminder.Core.Interfaces.Services
{
    public interface IPlatformService
    {
        Task<bool> Post(string pushToken);
    }
}
