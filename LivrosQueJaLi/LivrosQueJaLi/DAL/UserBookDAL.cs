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
            List<UserBook> books = null;
            var bks = await _azureClient.Table
                .Where(b => b.IdUser == pIdUser
                && b.IsWish == pWish && b.IsRead != pWish)
                .ToListAsync();

            if (bks != null || bks.Count > 0)
            {
                books = new List<UserBook>();
                foreach (var book in bks)
                {
                    book.Book = await new GoogleBooksClient().GetBookByIdAsync(book.IdBook);
                    books.Add(book);
                }
            }

            return books;
        }
    }
}
