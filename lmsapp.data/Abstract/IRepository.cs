namespace lmsapp.data.Abstract
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
    }
}