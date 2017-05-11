using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class CommentDAL : IBaseDAL<Comment>
    {
        private AzureClient<Comment> _azureClient;

        public CommentDAL()
        {
            _azureClient = new AzureClient<Comment>();
        }

        public async void InsertOrUpdate(Comment obj)
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

        /// <summary>
        /// NÃO IMPLEMENTADO
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Comment SelectById(string pId) => throw new NotImplementedException();

        public async Task<List<Comment>> SelectBookComments(string pIdBook)
        {
            var comments = await _azureClient.Table().Where(c => c.IdBook == pIdBook).ToListAsync();
            return comments;
        }
    }
}
