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
                Token = "fxxRrWZOanZkOum3tgqhi-:APA91bFb5XiSi2sFqmgyS2mv24OJlF5T3M6IF_5wk9t3NpEvGsp4ICXvK0NGHAd66TuPNJL0TrayJLQOi9R-ol6xza-KDoeGmJ5IKsufJ-70PHbHicU1CBJ3dOX1Rj8ofh1oKe_OE233",
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
