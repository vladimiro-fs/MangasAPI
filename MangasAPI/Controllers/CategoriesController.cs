namespace MangasAPI.Controllers
{
    using AutoMapper;
    using MangasAPI.DTOs;
    using MangasAPI.Entities;
    using MangasAPI.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes ="Bearer")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories is null)
                return NotFound("Categories not found");

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name ="GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return NotFound("Category not found");

            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto is null)
                return BadRequest("Invalid information");

            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);

            return new CreatedAtRouteResult(
                "GetCategory", 
                new { id = categoryDto.Id }, 
                categoryDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest();

            if (categoryDto is null)
                return BadRequest();

            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateAsync(category);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return NotFound("Category not found");

            await _categoryRepository.RemoveAsync(id);

            return Ok(category);
        }
    }
}
