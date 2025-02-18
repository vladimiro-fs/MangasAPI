namespace MangasAPI.DTOs
{
    public class MangaPaginationResponseDTO
    {
        public List<MangaDTO>? Mangas { get; set; }

        public int TotalPages { get; set; }
    }
}
