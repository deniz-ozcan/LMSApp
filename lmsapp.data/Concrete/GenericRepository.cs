using Microsoft.EntityFrameworkCore;
using lmsapp.data.Abstract;

namespace lmsapp.data.Concrete
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        public GenericRepository(DbContext ctx)
        {
            context = ctx;
        }
        public async Task CreateAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }
        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }
        public Task<List<TEntity>> GetAllAsync()
        {
            return context.Set<TEntity>().ToListAsync();
        }
        public Task UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }
    }
}