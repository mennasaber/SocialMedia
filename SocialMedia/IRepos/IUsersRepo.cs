using SocialMedia.Data.Dtos;
using SocialMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.IRepos
{
    public interface IUsersRepo
    {
        Task<ResponseDto> Login(LoginDto loginDto);
        Task<ResponseDto> Register(RegisterDto registerDto);
    }
}
