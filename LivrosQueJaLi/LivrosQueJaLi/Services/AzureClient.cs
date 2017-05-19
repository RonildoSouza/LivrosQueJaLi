using LivrosQueJaLi.Authentication;
using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models.Entities;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.Services
{
    public class AzureClient<T>
    {
        private const string MobileAppUri = Constants.MobileAppUri;
        private IMobileServiceClient _client;

        private IMobileServiceTable<T> _table;
        public IMobileServiceTable<T> Table
        {
            get { return _table; }
        }

        public AzureClient()
        {
            _client = new MobileServiceClient(MobileAppUri);
            _table = _client.GetTable<T>();
        }

        public async Task<User> LoginAsync()
        {
            User user = null;

            try
            {
                var auth = DependencyService.Get<IAuthentication>();
                var msUser = await auth.LoginAsync(_client, MobileServiceAuthenticationProvider.Facebook);

                if (msUser != null)
                {
                    var profile = await _client
                        .InvokeApiAsync("/.auth/me", System.Net.Http.HttpMethod.Get, null)
                        .ConfigureAwait(false);

                    // Monta o array e busca o UserId e UserName do FB da estrutura JSON recebida.
                    var a = JArray.Parse(profile[0]["user_claims"].ToString());
                    string userId = "";
                    string userName = "";
                    for (int i = 0; i < a.Count; i++)
                    {
                        if (a[i].Value<string>("typ").Contains("claims/nameidentifier"))
                            userId = a[i].Value<string>("val");
                        else if (a[i].Value<string>("typ").Contains("claims/name"))
                            userName = a[i].Value<string>("val");
                    }

                    user = new User()
                    {
                        IdFacebook = userId,
                        UserName = userName
                    };
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        "Erro", "Login Cancelado!", "OK");
                    });
                }
            }
            catch (System.Exception e)
            {
                throw new System.Exception(e.Message);
            }

            return user;
        }
    }
}
