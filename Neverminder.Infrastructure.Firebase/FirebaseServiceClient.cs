using FirebaseAdmin.Messaging;
using Neverminder.Core.Interfaces.Infrastructure;

namespace Neverminder.Infrastructure.Firebase
{
    public class FirebaseServiceClient : IFirebaseServiceClient
    {
        public async Task<bool> IsValid(string pushToken)
        {
            try
            {
                await FirebaseMessaging.DefaultInstance.SendAsync(new Message { Token = pushToken }, true);
                return true;
            }
            catch { return false; }
        }
    }
}
