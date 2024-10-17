using AutoMapper;
using eScape.Core.Const;
using eScape.Entities;
using eScape.UseCase.DTOs;
using eScape.UseCase.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = _mapper.Map<Category>(categoryDto);
            bool isSuccess = await _categoryRepository.UpdateCategoryAsync(category, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            bool isSuccess = await _categoryRepository.UpdateCategoryAsync(category, Actions.Update);
            return isSuccess ? Ok("Updated successfully!") : BadRequest("Updated Failure!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            bool isSuccess = await _categoryRepository.UpdateCategoryAsync(category, Actions.Delete);
            return isSuccess ? Ok("Deleted successfully!") : BadRequest("Deleted Failure!");
        }
    }
}
