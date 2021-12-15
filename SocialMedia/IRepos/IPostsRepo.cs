using SocialMedia.Data.Dtos;
using SocialMedia.Data.ResponeViewModels;
using System.Threading.Tasks;

namespace SocialMedia.IRepos
{
    public interface IPostsRepo
    {
        Task<Response<PostDto>> AddPostAsync(BasePostDto basePostDto);
        Task<Response<PostDto>> GetPostByIdAsync(int id);
        Task<BaseResponse> DeletePostAsync(int id);
        ListResponse<PostDto> GetPostsForUser(string userId);
        Task<BaseResponse> UpdateAsync(int id,string text);
    }
}
