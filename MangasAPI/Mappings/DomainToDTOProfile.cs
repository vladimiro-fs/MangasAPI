namespace MangasAPI.Mappings
{
    using AutoMapper;
    using MangasAPI.DTOs;
    using MangasAPI.Entities;

    public class DomainToDTOProfile : Profile
    {
        public DomainToDTOProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Manga, MangaDTO>().ReverseMap();

            // Creates a mapping between Manga class and MangaCategoryDTO
            // The mapping specifies that the property CategoryName of the DTO
            // will be mapped from the Name property of the Category property
            // of the Manga object
            CreateMap<Manga, MangaCategoryDTO>()
                .ForMember(dto => dto.CategoryName, opt => 
                opt.MapFrom(src => src.Category.Name));
        }
    }
}
