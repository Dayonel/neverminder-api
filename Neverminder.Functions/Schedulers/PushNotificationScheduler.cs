using FirebaseAdmin.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Neverminder.Core.Entity;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Functions.Interfaces;
using Neverminder.Infrastructure.Firebase.Extensions;

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
                        var notification = new Notification { Title = f.Title, Body = f.Description };
                        var message = notification.Map(f.Platform.PushToken, f.Id);
                        messages.Add(message);
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
