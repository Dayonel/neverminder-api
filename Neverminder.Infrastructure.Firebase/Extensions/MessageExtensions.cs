using FirebaseAdmin.Messaging;

namespace Neverminder.Infrastructure.Firebase.Extensions
{
    public static class MessageExtensions
    {
        public static Message Map(this Notification notification, 
            string pushToken,
            int id) 
        {
            return notification != null
                ?
                new Message
                {
                    Apns = new ApnsConfig { Aps = new Aps { Sound = "default" } },
                    Android = new AndroidConfig { Priority = Priority.High, Notification = new AndroidNotification { ChannelId = "Neverminder", Sound = "default", Priority = NotificationPriority.MAX } },
                    Notification = notification,
                    Token = pushToken,
                    Data = new Dictionary<string, string> { { "id", id.ToString() } },
                }
                : null;
        }
    }
}
