using Microsoft.WindowsAzure.MobileServices;

namespace LivrosQueJaLi.Services
{
    public class AzureClient<T>
    {
        private const string MobileAppUri = "http://apppoc.azurewebsites.net";
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
    }
}
