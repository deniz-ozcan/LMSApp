namespace lmsapp.business.Abstract
{
    public interface IService<T> 
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
    }
}