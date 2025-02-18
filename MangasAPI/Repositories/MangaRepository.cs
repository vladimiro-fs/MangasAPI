namespace MangasAPI.Repositories
{
    using System.Threading.Tasks;
    using MangasAPI.Context;
    using MangasAPI.Entities;
    using MangasAPI.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class MangaRepository : Repository<Manga>, IMangaRepository
    {
        public MangaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Manga>> GetMangasByCategoryAsync(int categoryId)
        {
            var mangas = await _context.Mangas
                .Include(b => b.Category)
                .Where(b => b.CategoryId == categoryId).ToListAsync();

            return mangas;
        }

        public async Task<IEnumerable<Manga>> FindMangaWithCategoryAsync(string filter)
        {
            return await _context.Mangas.AsNoTracking()
                .Include(b => b.Category)
                .Where(b => b.Title.Contains(filter) ||
                       b.Description.Contains(filter) ||
                       b.Author.Contains(filter) ||
                       b.Category.Name.Contains(filter))
                .ToListAsync();
        }

        public IQueryable<Manga> GetMangasQueryable()
        {
            return _context.Mangas.AsQueryable();
        }
    }
}
