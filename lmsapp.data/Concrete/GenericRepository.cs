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
        public void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public bool Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return true;
        }

    }
}