using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;

namespace LivrosQueJaLi.Services
{
    public class AzureClient<T>
    {
        private const string MobileAppUri = "";
        private IMobileServiceClient _client;
        private IMobileServiceTable<T> _table;

        public IMobileServiceTable<T> GetTable()
        {
            try
            {
                _client = new MobileServiceClient(MobileAppUri);
                _table = _client.GetTable<T>();
            }
            catch (MobileServiceConflictException e)
            {
                Debug.WriteLine(e.Message);
            }

            return _table;
        }
    }
}
