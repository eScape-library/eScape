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
    public class ProductAttributeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductAttributeRepository _productAttributeRepository;
        public ProductAttributeController(IMapper mapper, IProductAttributeRepository productAttributeRepository)
        {
            _mapper = mapper;
            _productAttributeRepository = productAttributeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductAttributesAsync()
        {
            var productAttributes = await _productAttributeRepository.GetProductAttributesAsync();
            return Ok(productAttributes);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAttributeAsync([FromBody] ProductAttributeDTO productAttributeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productAttribute = _mapper.Map<ProductAttribute>(productAttributeDto);
            bool isSuccess = await _productAttributeRepository.UpdateProductAttributeAsync(productAttribute, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProductAttributeAsync([FromBody] ProductAttributeDTO productAttributeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productAttribute = _mapper.Map<ProductAttribute>(productAttributeDto);
            bool isSuccess = await _productAttributeRepository.UpdateProductAttributeAsync(productAttribute, Actions.Update);
            return isSuccess ? Ok("Updated successfully!") : BadRequest("Updated Failure!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductAttributeAsync([FromBody] ProductAttributeDTO productAttributeDto)
        {
            var productAttribute = _mapper.Map<ProductAttribute>(productAttributeDto);
            bool isSuccess = await _productAttributeRepository.UpdateProductAttributeAsync(productAttribute, Actions.Delete);
            return isSuccess ? Ok("Deleted successfully!") : BadRequest("Deleted Failure!");
        }
    }
}
