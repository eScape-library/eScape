using AutoMapper;
using eScape.Core.Const;
using eScape.Entities;
using eScape.UseCase.Repositories;
using eScape.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        public CartController(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetCartAsync(int userId)
        {
            var carts = await _cartRepository.GetCartAsync(userId);
            return Ok(carts);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCartAsync([FromBody] CartDTO cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart = _mapper.Map<Cart>(cartDto);
            bool isSuccess = await _cartRepository.UpdateCartAsync(cart, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCartAsync([FromBody] CartDTO cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart = _mapper.Map<Cart>(cartDto);
            bool isSuccess = await _cartRepository.UpdateCartAsync(cart, Actions.Update);
            return isSuccess ? Ok("Updated successfully!") : BadRequest("Updated Failure!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartAsync([FromBody] CartDTO cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            bool isSuccess = await _cartRepository.UpdateCartAsync(cart, Actions.Delete);
            return isSuccess ? Ok("Deleted successfully!") : BadRequest("Deleted Failure!");
        }
    }
}
