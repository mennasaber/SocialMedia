using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Data.Dtos;
using SocialMedia.Data.Models;
using SocialMedia.Data.ResponeViewModels;
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
        public async Task<AuthResponse<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
            {
                return new AuthResponse<UserDto> { Status = AppConstants.BadRequestStatus, Succeeded = false, Errors = new List<string> { AppConstants.UserExist } };
            }
            user = _mapper.Map(registerDto, user);
            user.DateRegistered = DateTime.Now;
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new AuthResponse<UserDto> { Status = AppConstants.CreatedStatus, Succeeded = true, Token = GenerateJwtToken(user), User = userDto };
            }
            return new AuthResponse<UserDto> { Status = AppConstants.BadRequestStatus, Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToList() };
        }
        //Password not change    
        public async Task<BaseResponse> UpdateAsync(string id, UserDto userDto)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (!userDto.Email.Equals(user.Email))
                return new BaseResponse { Status = AppConstants.BadRequestStatus, Succeeded = false, Errors = new List<string> { "Can't change user email" } };
            if (user != null)
            {
                _mapper.Map(userDto, user);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return new BaseResponse { Status = AppConstants.SuccessfulStatus, Succeeded = true };
                return new BaseResponse { Status = AppConstants.NotFoundStatus, Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToList() };
            }
            return new BaseResponse { Status = AppConstants.NotFoundStatus, Succeeded = false, Errors = new List<string> { "This's user doesn't exist" } };
        }
        public async Task<AuthResponse<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null)
            {
                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (isCorrectPassword)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return new AuthResponse<UserDto> { Status = AppConstants.SuccessfulStatus, Succeeded = true, Token = GenerateJwtToken(user), User = userDto };
                }
            }
            return new AuthResponse<UserDto> { Status = AppConstants.NotFoundStatus, Succeeded = false, Errors = new List<string> { AppConstants.InvalidUser } };
        }
        public ListResponse<UserDto> GetUsersByUsername(string searchKey)
        {
            if (!searchKey.Trim().Equals(""))
            {
                var users = _userManager.Users.Where(u => u.UserName.Contains(searchKey)).Select(u=>_mapper.Map<UserDto>(u)).ToList();
                return new ListResponse<UserDto> { Status = AppConstants.SuccessfulStatus, Succeeded = true, Entities = users };
            }
            return new ListResponse<UserDto> { Status = AppConstants.BadRequestStatus, Succeeded = false, Errors =new List<string> { "Search key is empty"} }; ;
        }
        private string GenerateJwtToken(User user)
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
