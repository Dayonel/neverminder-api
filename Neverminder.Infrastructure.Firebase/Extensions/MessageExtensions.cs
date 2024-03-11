using FirebaseAdmin.Messaging;

namespace Neverminder.Infrastructure.Firebase.Extensions
{
    public static class MessageExtensions
    {
        public static Message Map(this Notification notification, string pushToken) 
        {
            return notification != null
                ?
                new Message
                {
                    Apns = new ApnsConfig { Aps = new Aps { Sound = "default" } },
                    Android = new AndroidConfig { Priority = Priority.High, Notification = new AndroidNotification { ChannelId = "Neverminder", Sound = "default", Priority = NotificationPriority.MAX } },
                    Notification = notification,
                    Token = pushToken,
                    Data = new Dictionary<string, string> { { "url", "detail" } },
                }
                : null;
        }
    }
}
