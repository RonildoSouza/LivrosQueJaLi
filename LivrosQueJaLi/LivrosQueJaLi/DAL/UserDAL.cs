using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
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
                if (string.IsNullOrEmpty(obj.Id))
                    await _azureClient.Table.InsertAsync(obj);
                else
                    await _azureClient.Table.UpdateAsync(obj);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> SelectByIdFacebookOrEmailAsync(string pIdFacebook, string pEmail)
        {
            var usr = await _azureClient.Table
                .Where(u => u.IdFacebook == pIdFacebook || u.Email == pEmail)
                .ToListAsync();

            return usr.FirstOrDefault();
        }

        public async Task<User> SelectByIdAsync(string pIdUser)
        {
            var usr = await _azureClient.Table
                .Where(u => u.Id == pIdUser)
                .ToListAsync();

            return usr.FirstOrDefault();
        }
    }
}
