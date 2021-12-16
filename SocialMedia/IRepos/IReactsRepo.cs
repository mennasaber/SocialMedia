using SocialMedia.Data.Dtos;
using SocialMedia.Data.ResponeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.IRepos
{
    public interface IReactsRepo
    {
        Task<ListResponse<ReactDto>> GetReactsByPostIdAsync(int postId);
        Task<ListResponse<ReactDto>> GetReactsByUserIdAsync(string userId);
        Task<BaseResponse> DeleteReactAsync(int id);
        Task<Response<ReactDto>> AddReactAsync(BaseReactDto baseReactDto);
    }
}
