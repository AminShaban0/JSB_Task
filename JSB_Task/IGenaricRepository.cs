using JSB_Task.Models;

namespace JSB_Task
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
