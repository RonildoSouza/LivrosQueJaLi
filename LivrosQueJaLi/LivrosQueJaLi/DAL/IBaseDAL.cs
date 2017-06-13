using LivrosQueJaLi.Models.Entities;

namespace LivrosQueJaLi.DAL
{
    public interface IBaseDAL<T> where T : BaseEntity
    {
        void InsertOrUpdate(T obj);
    }
}
