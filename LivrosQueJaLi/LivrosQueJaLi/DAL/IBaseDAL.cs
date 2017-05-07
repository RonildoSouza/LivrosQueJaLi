using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.DAL
{
    public interface IBaseDAL<T>
    {
        void InsertOrUpdate(T obj);
        T SelectById(string pId);
    }
}
