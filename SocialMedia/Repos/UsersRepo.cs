using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Data.Dtos;
using SocialMedia.Data.Models;
using SocialMedia.IRepos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Repos
{
    public class UsersRepo : IUsersRepo
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsersRepo(UserManager<User> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        //Password not change 
        public async Task<Response> UpdateAsync(string id, UserDto userDto)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (!userDto.Email.Equals(user.Email))
                return new Response { Status = AppConstants.BadRequestStatus, Succeeded = false, Errors = new List<string> { "Can't change user email" } };
            if (user != null)
            {
                _mapper.Map(userDto, user);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return new Response { Status = AppConstants.SuccessfulStatus, Succeeded = true };
                return new Response { Status = AppConstants.NotFoundStatus, Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToList() };
            }
            return new Response { Status = AppConstants.NotFoundStatus, Succeeded = false, Errors = new List<string> { "This's user doesn't exist" } };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null)
            {
                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (isCorrectPassword)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return new AuthResponseDto { Status = AppConstants.SuccessfulStatus, Succeeded = true, Token = GenerateJwtToken(user), User = userDto };
                }
            }
            return new AuthResponseDto { Status = AppConstants.NotFoundStatus, Succeeded = false, Errors = new List<string> { AppConstants.InvalidUser } };
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
            {
                return new AuthResponseDto { Status = AppConstants.BadRequestStatus, Succeeded = false, Errors = new List<string> { AppConstants.UserExist } };
            }
            user = _mapper.Map(registerDto, user);
            user.DateRegistered = DateTime.Now;
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new AuthResponseDto { Status = AppConstants.CreatedStatus, Succeeded = true, Token = GenerateJwtToken(user), User = userDto };
            }
            return new AuthResponseDto { Status = AppConstants.BadRequestStatus, Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToList() };
        }
        string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
