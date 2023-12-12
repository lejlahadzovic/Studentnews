using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;
namespace StudentOglasi.Helper
{
    public class FirebaseCloudMessaging
    {
        private static FirebaseApp _firebaseApp;

        public static void InitializeFirebaseApp()
        {
            if (_firebaseApp == null)
            {
                _firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase-key.json")),
                });
            }
        }

        public static async Task<string> SendNotification(string title, string message, string notificationType)
        {

            var notificationMessage = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = message
                },
                Token = "eAj9oNCTf0Cy8S77sKWL4I:APA91bES1LRLtvnwy92oMzNfHjPmeARxeQSVc6NZekNi9NwIkh2fL1HZU0Fk6msZHfgfiCJyhpqCfMJpoZxnzTVK7vU3rAAnpQQ7BZwDI9w9URSCOn5nShDODscBB3_F1lDzTrr5Frta",
                Data = new Dictionary<string, string>()
                {
                    { "notificationType", notificationType }
                }
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(notificationMessage);

            return result;
        }
    }
}
