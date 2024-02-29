using FirebaseAdmin.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Neverminder.Core.Entity;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Functions.Interfaces;

namespace Neverminder.Functions.Schedulers
{
    public class PushNotificationScheduler : ICronJob
    {
        private readonly ILogger<PushNotificationScheduler> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public PushNotificationScheduler(ILogger<PushNotificationScheduler> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task Run(CancellationToken token = default)
        {
            try
            {
                var page = 1;
                var pageSize = 500; // max allowed by firebase
                var reminders = new List<Reminder>();

                using var scope = _scopeFactory.CreateScope();
                var _reminderRepository = scope.ServiceProvider.GetRequiredService<IReminderRepository>();

                do
                {
                    reminders = await _reminderRepository.ListPushNotifications(page, pageSize);
                    if (!reminders.Any())
                    {
                        return;
                    }

                    var messages = new List<Message>();
                    reminders.ForEach(f =>
                    {
                        messages.Add(new Message
                        {
                            Token = f.Platform.PushToken,
                            Notification = new Notification
                            {
                                Title = f.Title,
                                Body = f.Description,
                                ImageUrl = "https://github-production-user-asset-6210df.s3.amazonaws.com/10290812/304105150-b64fdd09-5297-4cf5-bb68-3d7249a9c147.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240224%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240224T021342Z&X-Amz-Expires=300&X-Amz-Signature=234223cfc30ea28775d42ecce0d2ce3507c163086cb58a247897aa67201a1e95&X-Amz-SignedHeaders=host&actor_id=10290812&key_id=0&repo_id=755372213"
                            }
                        });
                    });

                    var response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);
                    if (response.FailureCount > 0)
                    {
                        response.Responses.Where(w => !w.IsSuccess).ToList().ForEach(f =>
                        {
                            _logger.LogCritical($"{nameof(PushNotificationScheduler)}: {f.Exception}");
                        });
                    }

                    var sent = await _reminderRepository.MarkSent(reminders);
                    if (!sent)
                    {
                        _logger.LogCritical("Failed to mark sent.");
                        return;
                    }

                    page++;
                }
                while (reminders.Any());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
    }
}
