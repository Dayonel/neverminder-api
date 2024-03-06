using Neverminder.Core.Entity;
using Neverminder.Core.Interfaces.Infrastructure;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Core.Interfaces.Services;

namespace Neverminder.Core.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IFirebaseServiceClient _firebaseServiceClient;
        public PlatformService(IPlatformRepository platformRepository,
            IFirebaseServiceClient firebaseServiceClient)
        {
            _platformRepository = platformRepository;
            _firebaseServiceClient = firebaseServiceClient;
        }

        public async Task<bool> Post(string pushToken)
        {
            var exist = await _platformRepository.AnyAsync(a => a.PushToken == pushToken);
            if (exist) return true;

            var valid = await _firebaseServiceClient.IsValid(pushToken);
            if (!valid) return false;

            return await _platformRepository.AddAsync(new Platform
            {
                PushToken = pushToken,
            }) > 0;
        }

        public async Task<bool> Send(string pushToken)
        {
            return await _firebaseServiceClient.Send(pushToken);
        }
    }
}
