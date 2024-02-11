namespace Neverminder.Core.Interfaces.Infrastructure
{
    public interface IFirebaseServiceClient
    {
        Task<bool> IsValid(string pushToken);
    }
}
