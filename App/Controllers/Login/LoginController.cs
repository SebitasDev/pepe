using backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models.DTOs;

namespace backend.App.Controllers.Login
{
    public class LoginController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly MongoDbContext _context;
        public LoginController(MongoDbContext context, ITokenRepository tokenServices)
        {
            _context = context;
            _tokenRepository = tokenServices;
        }

        [HttpPost("riwitalent/login")]
        public async Task<IActionResult> Login([FromBody] TokenResponseDto tokenResponseDto)
        {
            var users = await _context.Users.Find(u => u.Email == tokenResponseDto.Email).FirstOrDefaultAsync();

            if (users == null)
            {
                return NotFound("Usuario o contraseña incorrectos.");
            }

            if (users == null || users.Password != users.Password)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            var token = _tokenRepository.GetToken(users);

            return Ok(new { Token = token });
        }
    }
}