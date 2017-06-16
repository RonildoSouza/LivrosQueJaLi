using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class NegotiationDAL : IBaseDAL<Negotiation>
    {
        private UserBookDAL _userBookDAL;
        private AzureClient<Negotiation> _azureClient;

        public NegotiationDAL()
        {
            _userBookDAL = new UserBookDAL();
            _azureClient = new AzureClient<Negotiation>();
        }

        public async void InsertOrUpdate(Negotiation obj)
        {
            try
            {
                await _azureClient.Table.InsertAsync(obj).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Negotiation>> SelectNegotiations(string pIdUserNegotiator, string pIdBook, string pIdUserInterested)
        {
            var userBook = await _userBookDAL.SelectUserBookByIds(pIdUserNegotiator, pIdBook);
            var negotiations = await _azureClient.Table
                .Where(n => n.IdUserBook == userBook.Id && n.IdUserInterested == pIdUserInterested)
                .OrderBy(n => n.CreatedAt)?.ToListAsync();

            return negotiations;
        }

        public async Task<List<Negotiation>> SelectAll() =>
            await _azureClient.Table.ToListAsync().ConfigureAwait(false);

        public async void Delete(Negotiation pNegotiation) =>
            await _azureClient.Table.DeleteAsync(pNegotiation).ConfigureAwait(false);
    }
}
