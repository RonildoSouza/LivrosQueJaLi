using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models;
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
        private UserBookDAL _userBookDAL;
        private AzureClient<User> _azureClient;

        public UserDAL()
        {
            _userBookDAL = new UserBookDAL();
            _azureClient = new AzureClient<User>();
        }

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

        public async Task<List<User>> SelectUsersWhoReadBookAsync(Book pBook)
        {
            List<User> users = await _azureClient.Table.ToListAsync();
            List<UserBook> userBooks = await _userBookDAL.SelectAll();

            //var usersRead = users.Join(userBooks,
            //    u => u.Id,
            //    ub => ub.IdUser,
            //    (u, ub) => new { User = u, UserBook = ub })
            //    .Where(j => j.UserBook.IdBook == pIdBook)
            //    .ToList();

            var usersRead = from u in users
                            join ub in userBooks on u.Id equals ub.IdUser
                            where ub.IdBook == pBook.Id && ub.IsRead == true
                            select u;

            return usersRead?.ToList();
        }
    }
}
