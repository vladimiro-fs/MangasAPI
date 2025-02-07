namespace MangasAPI.Repositories
{
    using System.Linq.Expressions;
    using MangasAPI.Context;
    using MangasAPI.Entities;
    using MangasAPI.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int? id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
