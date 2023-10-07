namespace lmsapp.data.Abstract
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<List<T>> GetAllAsync();
        void Delete(T entity);
    }
}