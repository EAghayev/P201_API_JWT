using BookStore.Api.Apps.AdminApi.DTOs.AccountDtos;
using BookStore.Core.Entities;
using BookStore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Api.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountsController(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName && x.IsAdmin);

            if (admin == null) return NotFound();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id),
                new Claim(ClaimTypes.Name,admin.UserName),
                new Claim("FullName",admin.FullName)
            };

            var adminRoles = _userManager.GetRolesAsync(admin).Result;
            var roleClaims = adminRoles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            string keyStr = _configuration.GetSection("JWT:secretKey").Value;
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
                (
                    claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.UtcNow.AddDays(3),
                    issuer: _configuration.GetSection("JWT:issuer").Value,
                    audience: _configuration.GetSection("JWT:audience").Value
                    );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenStr });
        }


    }
}
