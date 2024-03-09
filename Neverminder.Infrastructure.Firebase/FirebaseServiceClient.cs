﻿using FirebaseAdmin.Messaging;
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

        public async Task<bool> Send(string pushToken)
        {
            try
            {
                var notification = new Notification
                {
                    Title = "Lorem Ipsum",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur"
                };

                await FirebaseMessaging.DefaultInstance.SendAsync(new Message { Android = new AndroidConfig {  Notification = new AndroidNotification { ChannelId = "Neverminder" } }, Notification = notification, Token = pushToken, Data = new Dictionary<string, string> { { "url", "detail" } } });
                return true;
            }
            catch { return false; }
        }
    }
}
