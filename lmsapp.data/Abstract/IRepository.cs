namespace lmsapp.data.Abstract
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        void Create(T entity);
        bool Update(T entity);
        void Delete(T entity);
    }
}