using RestaurantSystem.Application.DTOs;
using RestaurantSystem.Application.Interfaces;
using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace RestaurantSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository auth;
        private readonly IConfiguration conf;

        public AuthService(IAuthRepository auth , IConfiguration conf)
        {
            this.auth = auth;
            this.conf = conf;
        }

        public async Task<ResponseLoginDTO> LoginAsync(LoginDTO loginDTO)
        {
            
            var user = await auth.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await auth.CheckPasswordAsync(user, loginDTO.Password))
            {
                return null;
            }

            var roles = await auth.GetRolesAsync(user);

            var jwtsetting = conf.GetSection("Jwt");

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["Jwt:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256 );
            var token = new JwtSecurityToken(

            audience: jwtsetting["Audience"],
            issuer: jwtsetting["Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtsetting["DurationInMinutes"]!)),
            signingCredentials: cred
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponseLoginDTO
            {
                Token = tokenString,
                //UserId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Role = roles.ToList()
            };
        }

        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,  
                FullName = registerDTO.FullName,

            };
            var res = await auth.RegisterAsync(user , registerDTO.Password);

            if (!res.Succeeded)
            {
                return string.Join(", ", res.Errors.Select(e => e.Description));
            }

            await auth.AddToRoleAsync(user , "Customer");
           // await auth.RegisterAsync(user,registerDTO.Password);
            return "User Created Successfullty As a Customer"; 
        }


    }
}
