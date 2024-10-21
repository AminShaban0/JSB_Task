using JSB_Task.Data;
using JSB_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace JSB_Task
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly APPDbContext _dbContext;

        public GenaricRepository(APPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Order))
            {
                return (IReadOnlyList<T>)await _dbContext.Set<Order>().AsNoTracking().Include(E => E.OrderProducts).ThenInclude(x=>x.Product).ToListAsync();
            }

            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Order))
            {
                return await _dbContext.Set<Order>().AsNoTracking().Where(e => e.Id == id).Include(e => e.OrderProducts).ThenInclude(x=>x.Product).FirstOrDefaultAsync() as T;
            }

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
