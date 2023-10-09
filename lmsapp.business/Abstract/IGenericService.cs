namespace lmsapp.business.Abstract
{
    public interface IGenericService<T> 
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
    }
}