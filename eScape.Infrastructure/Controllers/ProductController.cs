using AutoMapper;
using eScape.Core.Const;
using eScape.UseCase.DTOs;
using eScape.Entities;
using eScape.UseCase.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(productDto);
            bool isSuccess = await _productRepository.UpdateProductAsync(product, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(productDto);
            bool isSuccess = await _productRepository.UpdateProductAsync(product, Actions.Update);
            return isSuccess ? Ok("Updated successfully!") : BadRequest("Updated Failure!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync([FromBody] ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            bool isSuccess = await _productRepository.UpdateProductAsync(product, Actions.Delete);
            return isSuccess ? Ok("Deleted successfully!") : BadRequest("Deleted Failure!");
        }
    }
}
