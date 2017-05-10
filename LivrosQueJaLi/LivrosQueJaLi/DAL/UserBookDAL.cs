using LivrosQueJaLi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public class UserBookDAL : IBaseDAL<UserBook>
    {
        public void InsertOrUpdate(UserBook obj)
        {
            throw new NotImplementedException();
        }

        public UserBook SelectById(string pId)
        {
            throw new NotImplementedException();
        }

        public List<UserBook> SelectUserBooks(string pIdUser, bool pWish = false)
        {
            return null;
        }
    }
}
