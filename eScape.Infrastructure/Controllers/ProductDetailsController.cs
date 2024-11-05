using AutoMapper;
using eScape.Core.Const;
using eScape.Entities;
using eScape.UseCase;
using eScape.UseCase.DTOs;
using eScape.UseCase.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductDetailsRepository _productDetailsRepository;
        public ProductDetailsController(IMapper mapper, IProductDetailsRepository productDetailsRepository)
        {
            _mapper = mapper;
            _productDetailsRepository = productDetailsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductDetailsAsync()
        {
            var productDetailss = await _productDetailsRepository.GetProductDetailsAsync();
            return Ok(productDetailss);
        }

        [HttpGet("{productDetailsId:int}")]
        public async Task<IActionResult> GetAProductDetailsAsync(int productDetailsId)
        {
            var productDetailss = await _productDetailsRepository.GetProductDetailsByIdAsync(productDetailsId);
            return Ok(productDetailss);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetailsAsync([FromBody] ProductDetailsDTO productDetailsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productDetails = _mapper.Map<ProductDetails>(productDetailsDto);
            bool isSuccess = await _productDetailsRepository.UpdateProductDetailsAsync(productDetails, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProductDetailsAsync([FromBody] ProductDetailsDTO productDetailsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productDetails = _mapper.Map<ProductDetails>(productDetailsDto);
            bool isSuccess = await _productDetailsRepository.UpdateProductDetailsAsync(productDetails, Actions.Update);
            return isSuccess ? Ok("Updated successfully!") : BadRequest("Updated Failure!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetailsAsync([FromBody] ProductDetailsDTO productDetailsDto)
        {
            var productDetails = _mapper.Map<ProductDetails>(productDetailsDto);
            bool isSuccess = await _productDetailsRepository.UpdateProductDetailsAsync(productDetails, Actions.Delete);
            return isSuccess ? Ok("Deleted successfully!") : BadRequest("Deleted Failure!");
        }
    }
}
