using AutoMapper;
using eScape.Core.Const;
using eScape.Entities;
using eScape.UseCase.Repositories;
using eScape.UseCase;
using Microsoft.AspNetCore.Mvc;
using eScape.UseCase.DTOs;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPromotionRepository _promotionRepository;
        public PromotionController(IMapper mapper, IPromotionRepository promotionRepository)
        {
            _mapper = mapper;
            _promotionRepository = promotionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPromotionAsync()
        {
            var promotions = await _promotionRepository.GetPromotionsAsync();
            return Ok(promotions);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePromotionAsync([FromBody] PromotionDTO promotionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotion = _mapper.Map<Promotion>(promotionDto);
            bool isSuccess = await _promotionRepository.UpdatePromotionAsync(promotion, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePromotionAsync([FromBody] PromotionDTO promotionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotion = _mapper.Map<Promotion>(promotionDto);
            bool isSuccess = await _promotionRepository.UpdatePromotionAsync(promotion, Actions.Update);
            return isSuccess ? Ok("Updated successfully!") : BadRequest("Updated Failure!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePromotionAsync([FromBody] PromotionDTO promotionDto)
        {
            var promotion = _mapper.Map<Promotion>(promotionDto);
            bool isSuccess = await _promotionRepository.UpdatePromotionAsync(promotion, Actions.Delete);
            return isSuccess ? Ok("Deleted successfully!") : BadRequest("Deleted Failure!");
        }
    }
}
