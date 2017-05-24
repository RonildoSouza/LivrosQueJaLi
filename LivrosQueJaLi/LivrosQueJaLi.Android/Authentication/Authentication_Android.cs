using System;
using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using LivrosQueJaLi.Droid.Authentication;
using LivrosQueJaLi.Authentication;
using Android.Webkit;

[assembly: Xamarin.Forms.Dependency(typeof(Authentication_Android))]
namespace LivrosQueJaLi.Droid.Authentication
{
    public class Authentication_Android : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(
            IMobileServiceClient pClient, MobileServiceAuthenticationProvider pProvider)
        {
            try
            {
                var msUser = await pClient.LoginAsync(Xamarin.Forms.Forms.Context, pProvider);
                return msUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task LogoutAsync(IMobileServiceClient pClient)
        {
            try
            {
                CookieManager.Instance.RemoveAllCookie();
                await pClient.LogoutAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}