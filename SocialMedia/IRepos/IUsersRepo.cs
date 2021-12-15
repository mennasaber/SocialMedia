using SocialMedia.Data.Dtos;
using SocialMedia.Data.ResponeViewModels;
using System.Threading.Tasks;

namespace SocialMedia.IRepos
{
    public interface IUsersRepo
    {
        Task<AuthResponse<UserDto>> LoginAsync(LoginDto loginDto);
        Task<AuthResponse<UserDto>> RegisterAsync(RegisterDto registerDto);
        Task<BaseResponse> UpdateAsync(string id,UserDto userDto);
        ListResponse<UserDto> GetUsersByUsername(string searchKey);
        Task<Respose<UserDto>> GetUserByIdAsync(string id);
    }
}
