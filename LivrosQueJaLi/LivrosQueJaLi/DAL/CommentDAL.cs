using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class CommentDAL : IBaseDAL<Comment>
    {
        private AzureClient<Comment> _azureClient;

        public CommentDAL() => _azureClient = new AzureClient<Comment>();

        public async void InsertOrUpdate(Comment obj)
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

        public async Task<List<Comment>> SelectBookCommentsAsync(string pIdBook)
        {
            var comments = await _azureClient.Table.Where(c => c.IdBook == pIdBook).ToListAsync();
            return comments;
        }
    }
}
