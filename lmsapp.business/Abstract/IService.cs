namespace lmsapp.business.Abstract
{
    public interface IService<T> 
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
    }
}