namespace MangasAPI.Repositories.Interfaces
{
    using MangasAPI.Entities;

    public interface IMangaRepository : IRepository<Manga>
    {
        Task<IEnumerable<Manga>> GetMangasByCategoryAsync(int categoryId);

        Task<IEnumerable<Manga>> FindMangaWithCategoryAsync(string filter);

        IQueryable<Manga> GetMangasQueryable();
    }
}
