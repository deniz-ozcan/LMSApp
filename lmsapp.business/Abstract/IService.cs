namespace lmsapp.business.Abstract
{
    public interface IService<T> 
    {
        Task<T> CreateAsync(T entity);
        bool Update(T entity);
        void Delete(T entity);
    }
}