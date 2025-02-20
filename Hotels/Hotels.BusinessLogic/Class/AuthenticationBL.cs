using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hotels.BusinessLogic.Interface;
using Hotels.DataAccess.Class;
using Hotels.DataAccess.Interface;
using Hotels.Entity.DTO;
using Hotels.Entity.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace Hotels.BusinessLogic.Class
{
    public class AuthenticationBL : IAuthentication, IDisposable
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IConfiguration configuration;
        private Repository<User> UserRepository;

        public AuthenticationBL(IUnitOfWork UoW, IConfiguration configuration)
        {
            this.UnitOfWork = UoW;
            this.configuration = configuration;
            UserRepository = UnitOfWork.Repository<User>();
        }

        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await UserRepository.FindByEntity(x => x.Email == loginDTO.Email);
            if (getUser == null) {
                return new LoginResponse(false, "User not found", null);
            }
            bool chechPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.PasswordHash);
            if (chechPassword) {
                return new LoginResponse(true, "Generated Token", GenerateJWTToken(getUser));
            } else {
                return new LoginResponse(false, "Invalid credentials", null);
            }
        }

        private string? GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Agency"),
            };
            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
