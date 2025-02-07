namespace MangasAPI.Repositories
{
    using MangasAPI.Context;
    using MangasAPI.Entities;
    using MangasAPI.Repositories.Interfaces;

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}
