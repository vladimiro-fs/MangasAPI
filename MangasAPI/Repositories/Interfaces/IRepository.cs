namespace MangasAPI.Repositories.Interfaces
{
    using MangasAPI.Entities;
    using System.Linq.Expressions;

    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task AddAsync(T entity);

        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int? id);

        Task UpdateAsync(T entity);

        Task RemoveAsync(int? id);

        // Method to perform assynchronous search on EF Core context
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
    }
}
