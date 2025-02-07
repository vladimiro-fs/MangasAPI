namespace MangasAPI.Repositories
{
    using MangasAPI.Context;
    using MangasAPI.Entities;
    using MangasAPI.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class MangaRepository : Repository<Manga>, IMangaRepository
    {
        public MangaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Manga>> GetMangasByCategoryAsync(int categoryId)
        {
            return await _context.Mangas.Where(b => b.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Manga>> TrackMangaWithCategoryAsync(string filter)
        {
            return await _context.Mangas.AsNoTracking()
                .Include(b => b.Category)
                .Where(b => b.Title.Contains(filter) ||
                       b.Description.Contains(filter) ||
                       b.Author.Contains(filter) ||
                       b.Category.Name.Contains(filter))
                .ToListAsync();
        }
    }
}
