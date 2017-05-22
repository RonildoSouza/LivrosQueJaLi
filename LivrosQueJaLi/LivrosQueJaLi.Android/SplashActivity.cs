using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace LivrosQueJaLi.Droid
{
    [Activity(Theme = "@style/Theme.Splash", //Indicates the theme to use for this activity
             MainLauncher = true, //Set it as boot activity
             NoHistory = true)] //Doesn't place it in back stack
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            System.Threading.Thread.Sleep(3000); //Let's wait awhile...
            this.StartActivity(typeof(MainActivity));
        }
    }
}