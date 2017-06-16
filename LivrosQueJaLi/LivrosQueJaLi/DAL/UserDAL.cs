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
        private NegotiationDAL _negotiationDAL;
        private AzureClient<User> _azureClient;

        public UserDAL()
        {
            _userBookDAL = new UserBookDAL();
            _negotiationDAL = new NegotiationDAL();
            _azureClient = new AzureClient<User>();
        }

        public async void InsertOrUpdate(User obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.Id))
                    await _azureClient.Table.InsertAsync(obj).ConfigureAwait(false);
                else
                    await _azureClient.Table.UpdateAsync(obj).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> SelectByIdFacebookOrEmailAsync(string pIdFacebook, string pEmail)
        {
            var usr = await _azureClient.Table
                    .Where(u => u.Email == pEmail || u.IdFacebook == pIdFacebook)
                    ?.ToListAsync();

            return usr.FirstOrDefault();
        }

        public async Task<User> SelectByIdAsync(string pIdUser)
        {
            var usr = await _azureClient.Table
                .Where(u => u.Id == pIdUser)
                ?.ToListAsync();

            return usr.FirstOrDefault();
        }

        public async Task<List<dynamic>> SelectUsersWhoReadBookAsync(Book pBook)
        {
            List<User> users = await _azureClient.Table.ToListAsync().ConfigureAwait(false);
            List<UserBook> userBooks = await _userBookDAL.SelectAll();

            var usersRead = users.Join(userBooks,
                u => u.Id,
                ub => ub.IdUser,
                (u, ub) => new { User = u, UserBook = ub })
                .Where(j => j.UserBook.IdBook == pBook.Id && j.UserBook.IsRead == true)
                .ToList();

            return usersRead?.ToList<dynamic>();
        }

        public async Task<List<dynamic>> SelectInterestedUsersBookAsync(UserBook pUserBook)
        {
            List<User> users = await _azureClient.Table.ToListAsync().ConfigureAwait(false);
            List<UserBook> userBooks = await _userBookDAL.SelectAll();
            List<Negotiation> negotiations = await _negotiationDAL.SelectAll();

            var list = from u in users
                       join ub in userBooks on u.Id equals ub.IdUser
                       join n in negotiations on ub.IdUser equals n.IdUserInterested
                       where n.IdUserBook == pUserBook.Id && ub.IdBook == pUserBook.IdBook
                       group n by new { n.IdUserInterested, User = u } into g
                       select g.Key;

            return list.ToList<dynamic>();
        }
    }
}
