using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Gcm.Client;

namespace LivrosQueJaLi.Droid
{
    [Activity(Label = "Livros Que Já Li", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region PushNotification
        // Create a new instance field for this activity. 
        static MainActivity instance = null;
        // Return the current activity instance. 
        public static MainActivity CurrentActivity { get { return instance; } }
        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            #region SocialLogin
            //Obtem o contexto para efetuar o login 
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            #endregion

            #region PushNotification
            instance = this;

            try
            {
                //Check to see that GCM is supported and that the manifest has the correct information
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);

                //Call to Register the device for Push Notifications
                GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            #endregion

            LoadApplication(new App());
        }
    }
}

