using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void InsertOrUpdate(Negotiation obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Negotiation>> SelectNegotiations(string pIdUser, string pIdBook)
        {
            var userBook = await _userBookDAL.SelectUserBookByIds(pIdUser, pIdBook);
            var negotiations = await _azureClient.Table
                .Where(n => n.IdUserBook == userBook.Id)
                .OrderByDescending(n => n.CreatedAt)?.ToListAsync();

            return negotiations;
        }

        //public async Task<List<Negotiation>> SelectInterestedUsers(string pIdUser, string pIdBook)
        //{
        //    _azureClient.Table.Where(n => n.)

        //    return null;
        //}
    }
}
