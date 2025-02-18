namespace MangasAPI.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Icon is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? IconCSS { get; set; }
    }
}
