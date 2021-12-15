using AutoMapper;
using SocialMedia.Data;
using SocialMedia.Data.Dtos;
using SocialMedia.Data.Models;
using SocialMedia.Data.ResponeViewModels;
using SocialMedia.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Repos
{
    public class PostsRepo : IPostsRepo
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public PostsRepo(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<PostDto>> AddPostAsync(BasePostDto basePostDto)
        {
            var post = _mapper.Map<Post>(basePostDto);
            post.DateCreated = DateTime.Now;
            await _context.Posts.AddAsync(post);
            _context.SaveChanges();
            var postDto = _mapper.Map<PostDto>(post);
            return new Response<PostDto> { Status = AppConstants.CreatedStatus, Succeeded = true, EntityDto = postDto };
        }

        public async Task<BaseResponse> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return new BaseResponse
                {
                    Status = AppConstants.SuccessfulStatus,
                    Succeeded = true
                };
            }
            return new BaseResponse
            {
                Status = AppConstants.NotFoundStatus,
                Succeeded = false,
                Errors = new List<string> { AppConstants.NotFoundMessage }
            };
        }

        public async Task<Response<PostDto>> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                var postDto = _mapper.Map<PostDto>(post);
                return new Response<PostDto>
                {
                    Status = AppConstants.SuccessfulStatus,
                    Succeeded = true,
                    EntityDto = postDto
                };
            }
            return new Response<PostDto>
            {
                Status = AppConstants.NotFoundStatus,
                Succeeded = false,
                Errors = new List<string> { AppConstants.NotFoundMessage }
            };
        }

        public ListResponse<PostDto> GetPostsForUser(string userId)
        {
            var postsDto = _context.Posts
                .Where(p => p.UserId == userId)
                .Select(p => _mapper.Map<PostDto>(p))
                .ToList();
            return new ListResponse<PostDto>
            {
                Status = AppConstants.SuccessfulStatus,
                Succeeded = true,
                Entities = postsDto
            };
        }
        
        public async Task<BaseResponse> UpdateAsync(int id, string text)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                post.Text = text;
                _context.Posts.Update(post);
                _context.SaveChanges();
                return new BaseResponse
                {
                    Status = AppConstants.SuccessfulStatus,
                    Succeeded = true
                };
            }
            return new BaseResponse
            {
                Status = AppConstants.NotFoundStatus,
                Succeeded = false,
                Errors = new List<string> { AppConstants.NotFoundMessage }
            };
        }
    }
}
