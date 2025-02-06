namespace MangasAPI.Entities
{
    public class Manga : Entity
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Author { get; set; }

        public string? Publisher { get; set; }

        public int Pages { get; set; }

        public DateTime PublishDate { get; set; }

        public string? Format { get; set; }

        public string? Color { get; set; }

        public string? Origin { get; set; }

        public string? Image { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        // Relationship
        public Category? Category { get; set; }
    }
}
