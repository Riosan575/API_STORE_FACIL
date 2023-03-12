using ApiStore.DB;
using ApiStore.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ApiStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private StoreContext context;
        private IConfiguration config;
        public AuthController(IConfiguration config, StoreContext context)
        {
            this.context = context;
            this.config = config;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Usuario _user)
        {
            var user = await context.Usuarios.SingleOrDefaultAsync(o => o.username == _user.username && o.password == _user.password);
            if (user is null)
                return BadRequest(new { message = "Credenciales inválidas" });

            string jwtToken = GenerateToken(user);


            return Ok(new { token = jwtToken});
        }

        private string GenerateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
