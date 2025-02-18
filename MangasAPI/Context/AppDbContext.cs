namespace MangasAPI.Context
{
    using MangasAPI.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Manga> Mangas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            builder.Entity<Category>().HasKey(t => t.Id);
            builder.Entity<Category>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Category>()
                .Property(p => p.IconCSS)
                .HasMaxLength(100)
                .IsRequired();

            // 1 : N => Category : Mangas
            builder.Entity<Category>()
                .HasMany(c => c.Mangas)
                .WithOne(b => b.Category)
                .HasForeignKey(b => b.CategoryId);

            // defines the initial data for Category entity
            builder.Entity<Category>().HasData(
                new Category(1, "Adventure", "oi oi-aperture"),
                new Category(2, "Action", "oi oi-fire"),
                new Category(3, "Drama", "oi oi-cloudy"),
                new Category(4, "Romance", "oi oi-layers"),
                new Category(5, "Sci-Fi", "oi oi-tablet")
            );

            builder.Entity<Manga>().HasKey(t => t.Id);

            // defines properties max length
            builder.Entity<Manga>().Property(p => p.Title).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(p => p.Description).HasMaxLength(200).IsRequired();
            builder.Entity<Manga>().Property(p => p.Author).HasMaxLength(200).IsRequired();
            builder.Entity<Manga>().Property(p => p.Publisher).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(p => p.Format).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(p => p.Color).HasMaxLength(50).IsRequired();
            builder.Entity<Manga>().Property(p => p.Origin).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(p => p.Image).HasMaxLength(250).IsRequired();

            builder.Entity<Manga>().Property(p => p.Price).HasPrecision(10, 2);

            // foreign key deletion behavior (set null)
            foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) 
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
