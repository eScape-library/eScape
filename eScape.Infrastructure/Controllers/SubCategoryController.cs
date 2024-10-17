using AutoMapper;
using eScape.Core.Const;
using eScape.Entities;
using eScape.UseCase.DTOs;
using eScape.Infrastructure.SqlServer.Repositories;
using eScape.UseCase.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        public SubCategoryController(IMapper mapper, ISubCategoryRepository subCategoryRepository)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetSubCategoriesAsync()
        {
            var categories = await _subCategoryRepository.GetSubCategoriesAsync();
            var result = _mapper.Map<IEnumerable<SubCategoryDTO>>(categories);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubCategoryAsync([FromBody] SubCategoryDTO subCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            bool isSuccess = await _subCategoryRepository.UpdateSubCategoryAsync(subCategory, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSubCategoryAsync([FromBody] SubCategoryDTO subCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(subCategoryDto.CategoryId == 0)
            {
                return BadRequest("Category ID is required");
            }
            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            bool isSuccess = await _subCategoryRepository.UpdateSubCategoryAsync(subCategory, Actions.Update);
            return isSuccess ? Ok("Updated successfully!") : BadRequest("Updated Failure!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubCategoryAsync([FromBody] SubCategoryDTO subCategoryDto)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            bool isSuccess = await _subCategoryRepository.UpdateSubCategoryAsync(subCategory, Actions.Delete);
            return isSuccess ? Ok("Deleted successfully!") : BadRequest("Deleted Failure!");
        }
    }
}
