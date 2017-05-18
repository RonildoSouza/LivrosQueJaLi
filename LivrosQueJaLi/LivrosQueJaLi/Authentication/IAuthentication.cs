using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace LivrosQueJaLi.Authentication
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginAsync(IMobileServiceClient pClient, MobileServiceAuthenticationProvider pProvider);
    }
}
