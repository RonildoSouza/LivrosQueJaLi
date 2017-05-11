namespace LivrosQueJaLi.DAL
{
    public interface IBaseDAL<T>
    {
        void InsertOrUpdate(T obj);
    }
}
