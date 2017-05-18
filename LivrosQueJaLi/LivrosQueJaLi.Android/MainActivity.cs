using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LivrosQueJaLi.Droid.Authentication;
using Xamarin.Forms;
using LivrosQueJaLi.Authentication;

namespace LivrosQueJaLi.Droid
{
    [Activity(Label = "LivrosQueJaLi", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            //Obtem o contexto para efetuar o login
            ((Authentication_Android)DependencyService.Get<IAuthentication>()).Init(this);
        }
    }
}

