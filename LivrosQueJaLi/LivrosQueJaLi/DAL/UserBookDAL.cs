using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class UserBookDAL : IBaseDAL<UserBook>
    {
        private AzureClient<UserBook> _azureClient;

        public UserBookDAL() => _azureClient = new AzureClient<UserBook>();

        public async void InsertOrUpdate(UserBook obj)
        {
            try
            {
                if (obj.Id == null)
                    await _azureClient.Table.InsertAsync(obj);
                else
                    await _azureClient.Table.UpdateAsync(obj);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserBook> SelectUserBookByIds(string pIdUser, string pIdBook)
        {
            var bk = await _azureClient.Table
                .Where(b => b.IdUser == pIdUser && b.IdBook == pIdBook)
                .ToListAsync();

            return bk.FirstOrDefault();
        }

        public async Task<List<UserBook>> SelectUserBooksAsync(string pIdUser, bool pWish = false)
        {
            List<UserBook> newListUserBooks = new List<UserBook>();

            var userBooks = await _azureClient.Table
                .Where(ub => ub.IdUser == pIdUser
                && ub.IsWish == pWish && ub.IsRead != pWish)
                ?.ToListAsync();

            if (userBooks != null && userBooks.Count > 0)
            {
                newListUserBooks = new List<UserBook>();
                foreach (var userBook in userBooks)
                {
                    userBook.Book = await new GoogleBooksClient().GetBookByIdAsync(userBook.IdBook);
                    newListUserBooks.Add(userBook);
                }
            }

            return newListUserBooks
                .OrderBy(ub => ub.Book.VolumeInfo.Title)
                ?.ToList();
        }

        public async Task<List<UserBook>> SelectAll() =>
            await _azureClient.Table.ToListAsync();

        public async void DeleteUserBook(UserBook pUserBook) =>
            await _azureClient.Table.DeleteAsync(pUserBook);
    }
}
