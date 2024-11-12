using AutoMapper;
using Azure.Core;
using eScape.Core.Const;
using eScape.Core.Helper;
using eScape.Entities;
using eScape.Infrastructure.Services;
using eScape.UseCase.DTOs;
using eScape.UseCase.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eScape.Infrastructure.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        public AuthController(IMapper mapper, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(userDTO);
            user.Password = PasswordHasherHelper.HashPassword(user.Password);
            bool isSuccess = await _userRepository.UpdateUserAsync(user, Actions.Insert);
            return isSuccess ? Ok("Created successfully!") : BadRequest("Created Failure!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDTO? user = await _userRepository.GetAUserAsync(userDTO.UserName!);
                if (user == null || !PasswordHasherHelper.VerifyPassword(userDTO.Password!, user.Password!))
                {
                    return Unauthorized("Invalid credentials");
                }

                var jwtService = new JsonWebTokenService(_configuration);
                var accessToken = jwtService.GenerateJwtToken(user.UserId);
                var refreshToken = jwtService.GenerateRefreshToken();

                RefreshToken refreshTokenModule = new RefreshToken()
                {
                    UserName = userDTO.UserName,
                    Token = refreshToken,
                    ExpiryDate = DateTime.UtcNow.AddDays(7)
                };

                await _refreshTokenRepository.UpdateRefreshToken(refreshTokenModule, Actions.Insert);
                return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] RefreshTokenDTO refreshToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oldRefreshToken = _mapper.Map<RefreshToken>(refreshToken);
                await _refreshTokenRepository.UpdateRefreshToken(oldRefreshToken, Actions.Delete);
                return Ok("Logout successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenDTO refreshToken)
        {
            if (refreshToken == null || string.IsNullOrWhiteSpace(refreshToken.Token))
            {
                return BadRequest("Refresh token is required.");  
            }
            try
            {
                var username = await _refreshTokenRepository.RetrieveUserNameByRefreshToken(refreshToken.Token);
                if (string.IsNullOrEmpty(username))
                {
                    return Unauthorized("Invalid refresh token."); 
                }
                
                var user = await _userRepository.GetAUserAsync(username);
                if (user == null)
                {
                    return Unauthorized("Invalid user.");  
                }

                var oldRefreshToken = _mapper.Map<RefreshToken>(refreshToken);
                await _refreshTokenRepository.UpdateRefreshToken(oldRefreshToken, Actions.Delete);

                var jwtService = new JsonWebTokenService(_configuration);
                var accessToken = jwtService.GenerateJwtToken(user.UserId);
                var newRefreshToken = jwtService.GenerateRefreshToken();

                RefreshToken refreshTokenModule = new RefreshToken()
                {
                    UserName = user.UserName,
                    Token = newRefreshToken,
                    ExpiryDate = DateTime.UtcNow.AddDays(7)
                };

                await _refreshTokenRepository.UpdateRefreshToken(refreshTokenModule, Actions.Insert);

                return Ok(new { AccessToken = accessToken, RefreshToken = newRefreshToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");  
            }
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RefreshTokenDTO refreshToken)
        {
            if (refreshToken == null || string.IsNullOrWhiteSpace(refreshToken.Token))
            {
                return BadRequest("Refresh token is required.");  
            }
            try
            {
                RefreshToken refreshTokenModule = new RefreshToken()
                {
                    Token = refreshToken.Token
                };
                var result = await _refreshTokenRepository.UpdateRefreshToken(refreshTokenModule, Actions.Delete);
                if (!result)
                {
                    return NotFound("Refresh token not found.");  
                }
                return Ok("Token revoked."); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");  
            }
        }
    }

    
}
