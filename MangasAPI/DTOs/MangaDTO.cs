namespace MangasAPI.DTOs
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;
    using MangasAPI.Entities;

    public class MangaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required(ErrorMessage ="Description is required")]
        [MinLength(5)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage ="Author is required")]
        [MinLength(3)]
        [MaxLength(200)]
        public string? Author { get; set; }

        [Required(ErrorMessage ="Publisher is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Publisher { get; set; }

        [Required(ErrorMessage ="Page number is required")]
        [Range(1, 9999)]
        public int Pages { get; set; }

        [Required(ErrorMessage ="Publish date is required")]
        [DataType(DataType.Text)]
        [Display(Name ="Publish Date")]
        [DisplayFormat(DataFormatString ="{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage ="Format is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Format { get; set; }

        [Required(ErrorMessage ="Color is required")]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Color { get; set; }

        [Required(ErrorMessage ="Origin is required")]
        [MinLength(3)]
        [MaxLength(80)]
        public string? Origin { get; set; }

        [MaxLength(250)]
        public string? Image { get; set; }

        [Required(ErrorMessage ="Price is required")]
        [Column(TypeName ="decimal(10,2)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Stock is required")]
        [Range(1, 999)]
        public int Stock { get; set; }

        public Category? Category { get; set; }

        [DisplayName("Categories")]
        public int CategoryId { get; set; }
    }
}
