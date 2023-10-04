namespace lmsapp.data.Abstract
{
    public interface IRepository<T>
    {
        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}