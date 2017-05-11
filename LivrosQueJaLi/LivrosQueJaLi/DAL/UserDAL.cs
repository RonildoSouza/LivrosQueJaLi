using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class UserDAL : IBaseDAL<User>
    {
        private AzureClient<User> _azureClient;

        public UserDAL()
        {
            _azureClient = new AzureClient<User>();
        }
        public async void InsertOrUpdate(User obj)
        {
            try
            {
                await _azureClient.Table().InsertAsync(obj);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public User SelectById(string pId)
        {
            //var usr = _azureClient.Table.Where(u => u.Id == pId);
            return null;
        }
    }
}
