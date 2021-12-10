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
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(UserDto userDto);
        Task<Response> UpdateAsync(string id,UserDto userDto);

    }
}
