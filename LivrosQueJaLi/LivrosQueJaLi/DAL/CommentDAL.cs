using LivrosQueJaLi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class CommentDAL : IBaseDAL<Comment>
    {
        public void InsertOrUpdate(Comment obj)
        {
            throw new NotImplementedException();
        }

        public Comment SelectById(string pId)
        {
            throw new NotImplementedException();
        }

        public List<Comment> SelectBookComments(string pIdBook)
        {
            return null;
        }
    }
}
