using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Android.Util;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Android.Support.V7.App;
using Android.Media;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below 
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace LivrosQueJaLi.Droid
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project Number
        public static string[] SENDER_IDS = new string[] { "391818009539" };
    }

    [Service] //Must use the service tag
    public class GcmService : GcmServiceBase
    {
        MobileServiceClient client = new MobileServiceClient("http://apppushnotification.azurewebsites.net");
        public static string RegistrationID { get; private set; }

        public GcmService() : base(GcmBroadcastReceiver.SENDER_IDS) { }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
            RegistrationID = registrationId;

            var push = client.GetPush();
            MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info("PushHandlerBroadcastReceiver", "GCM Message Received!");
            //var msg = new StringBuilder();
            //if (intent != null && intent.Extras != null)
            //{
            //    foreach (var key in intent.Extras.KeySet())
            //        msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            //}

            ////Store the message 
            //var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            //var edit = prefs.Edit();
            //edit.PutString("last_msg", msg.ToString());
            //edit.Commit();

            string message = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(message))
            {
                CreateNotification("Livros Que Já li", message);
                return;
            }
        }

        protected override void OnError(Context context, string errorId)
        {
            throw new NotImplementedException();
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            throw new NotImplementedException();
        }

        public async void Register(Microsoft.WindowsAzure.MobileServices.Push push, IEnumerable<string> tags)
        {
            try
            {
                const string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\"}}";
                JObject templates = new JObject();
                templates["genericMessage"] = new JObject { { "body", templateBodyGCM } };

                await push.RegisterAsync(RegistrationID, templates);
                Log.Info("Push Installation Id", push.InstallationId.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }

        void CreateNotification(string title, string desc)
        {
            //Create notification 
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show ui 
            var uiIntent = new Intent(this, typeof(MainActivity));

            //Use Notification Builder 
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            //Create the notification 
            //we use the pending intent, passing our ui intent over which will get called 
            //when the notification is tapped. 
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                //.SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetSmallIcon(Resource.Drawable.icon_np)
                .SetTicker(title)
                .SetContentTitle(title)
                .SetContentText(desc)

                //Set the notification sound 
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))

                //Auto cancel will remove the notification once the user touches it 
                .SetAutoCancel(true).Build();

            //Show the notification 
            notificationManager.Notify(1, notification);
        }
    }
}