using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class UserDAL : IBaseDAL<User>
    {
        private AzureClient<User> _azureClient;

        public UserDAL() => _azureClient = new AzureClient<User>();

        public async void InsertOrUpdate(User obj)
        {
            try
            {
                await _azureClient.Table.InsertAsync(obj);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> SelectByIdFacebookAsync(string pIdFacebook)
        {
            var usr = await _azureClient.Table
                .Where(u => u.IdFacebook == pIdFacebook)
                .ToListAsync();

            return usr.FirstOrDefault();
        }
    }
}
