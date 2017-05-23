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
        private Context _context;

        public void Init(Context pContext) => _context = pContext;

        public async Task<MobileServiceUser> LoginAsync(
            IMobileServiceClient pClient, MobileServiceAuthenticationProvider pProvider)
        {
            try
            {
                var msUser = await pClient.LoginAsync(_context, pProvider);
                return msUser;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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