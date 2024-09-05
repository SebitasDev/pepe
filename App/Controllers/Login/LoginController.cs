using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models;

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
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var users = await _context.Users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();

            if (users == null)
            {
                return NotFound("Usuario o contraseña incorrectos.");
            }

            if (user == null || user.Password != users.Password)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            var token = _tokenRepository.GetToken(user);

            return Ok(new { Token = token });
        }
    }
}