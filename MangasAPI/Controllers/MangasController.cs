namespace MangasAPI.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using MangasAPI.DTOs;
    using MangasAPI.Entities;
    using MangasAPI.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class MangasController : ControllerBase
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;

        public MangasController(IMangaRepository mangaRepository, IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var mangas = await _mangaRepository.GetAllAsync();
            if (mangas is null)
                return NotFound("Mangas not found");

            var mangasDto = _mapper.Map<IEnumerable<MangaDTO>>(mangas);
            return Ok(mangasDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById (int id)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);
            if (manga is null)
                return NotFound($"Manga with {id} not found");

            var mangaDto = _mapper.Map<MangaDTO>(manga);
            return Ok(mangaDto);
        }

        [HttpGet]
        [Route("getmangasbycategory/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMangasByCategory(int categoryId)
        {
            var mangas = await _mangaRepository.GetMangasByCategoryAsync(categoryId);
            if (!mangas.Any())
                return NotFound();

            var mangasDto = _mapper.Map<IEnumerable<MangaDTO>>(mangas);
            return Ok(mangasDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Add(MangaDTO mangaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var manga = _mapper.Map<Manga>(mangaDto);
            await _mangaRepository.AddAsync(manga);

            var newManga = _mapper.Map<MangaDTO>(manga);
            return Ok(newManga);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, MangaDTO mangaDto)
        {
            if (id != mangaDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var manga = _mapper.Map<Manga>(mangaDto);
            await _mangaRepository.UpdateAsync(manga);

            return Ok(mangaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Remove(int id)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);
            if (manga is null)
                return NotFound();

            await _mangaRepository.RemoveAsync(id);
            return Ok(manga);
        }

        [HttpGet]
        [Route("search/{mangaTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MangaDTO>>> Search(string mangaTitle)
        {
            var mangas = await _mangaRepository.SearchAsync(m => m.Title.Contains(mangaTitle));
            if (mangas is null)
                return NotFound("No mangas found");

            var mangasDto = _mapper.Map<IEnumerable<MangaDTO>>(mangas);
            return Ok(mangasDto);
        }

        [HttpGet]
        [Route("search-manga-with-category/{filter}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MangaCategoryDTO>>> SearchMangaWithCategory(string filter)
        {
            var mangasWithCategory = await _mangaRepository.FindMangaWithCategoryAsync(filter);

            var mangas = _mapper.Map<List<Manga>>(mangasWithCategory);
            if (mangas.Count == 0)
                return NotFound("No mangas found");

            var mangasCategoryDto = _mapper.Map<IEnumerable<MangaCategoryDTO>>(mangas);

            return Ok(mangasCategoryDto);
        }
    }
}
