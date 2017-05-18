using System;
using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using LivrosQueJaLi.Droid.Authentication;
using LivrosQueJaLi.Authentication;

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
            catch (Exception)
            {
                throw;
            }
        }
    }
}