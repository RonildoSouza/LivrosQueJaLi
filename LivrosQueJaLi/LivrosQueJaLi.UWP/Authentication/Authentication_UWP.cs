using LivrosQueJaLi.Authentication;
using LivrosQueJaLi.UWP.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Authentication_UWP))]
namespace LivrosQueJaLi.UWP.Authentication
{
    public class Authentication_UWP : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(
            IMobileServiceClient pClient, MobileServiceAuthenticationProvider pProvider)
        {
            try
            {
                var msUser = await pClient.LoginAsync(pProvider);
                return msUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
