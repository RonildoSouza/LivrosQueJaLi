using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class FriendDAL : IBaseDAL<Friend>
    {
        private AzureClient<Friend> _azureClient;
        private UserDAL _userDAL;

        public FriendDAL()
        {
            _azureClient = new AzureClient<Friend>();
            _userDAL = new UserDAL();
        }

        public async void InsertOrUpdate(Friend obj)
        {
            try
            {
                await _azureClient.Table.InsertAsync(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void Delete(Friend obj)
        {
            try
            {
                obj = await SelectFriend(obj.IdUser, obj.IdUserFriend);
                await _azureClient.Table.DeleteAsync(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<Friend> SelectFriend(string pIdUser, string pIdUserFacebook)
        {
            var friend = await _azureClient.Table
                .Where(f => f.IdUser == pIdUser && f.IdUserFriend == pIdUserFacebook).ToListAsync();

            return friend.FirstOrDefault();
        }

        public async Task<List<User>> SelectUserFriendsAsync(User pUser)
        {
            List<User> users = new List<User>();

            var friends = await _azureClient.Table
                .Where(f => f.IdUser == pUser.Id)
                .ToListAsync();

            foreach (var friend in friends)
                users.Add(await _userDAL.SelectByIdAsync(friend.IdUserFriend));

            return users;
        }
    }
}
