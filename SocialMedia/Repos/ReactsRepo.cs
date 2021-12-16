using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ReactsRepo : IReactsRepo
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ReactsRepo(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<ReactDto>> AddReactAsync(BaseReactDto baseReactDto)
        {
            var react = await _context.Reacts.SingleAsync(r => r.UserId == baseReactDto.UserId && r.PostId == baseReactDto.PostId);
            if (react == null)
            {
                react = _mapper.Map<React>(baseReactDto);
                react.DateCreated = DateTime.Now;
                await _context.Reacts.AddAsync(react);
                await _context.SaveChangesAsync();
                var reactDto = _mapper.Map<ReactDto>(react);
                return new Response<ReactDto>
                {
                    Status = AppConstants.CreatedStatus,
                    Succeeded = true,
                    EntityDto = reactDto
                };
            }
            return new Response<ReactDto>
            {
                Status = AppConstants.BadRequestStatus,
                Succeeded = false,
                Errors = new List<string> { AppConstants.ReactExist }
            };
        }

        public async Task<BaseResponse> DeleteReactAsync(int id)
        {
            var react = await _context.Reacts.FindAsync(id);
            if (react != null)
            {
                _context.Reacts.Remove(react);
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

        public async Task<ListResponse<ReactDto>> GetReactsByPostIdAsync(int postId)
        {
            var reactsDto = await _context.Reacts
                .Where(r => r.PostId == postId)
                .Select(r => _mapper.Map<ReactDto>(r))
                .ToListAsync();
            return new ListResponse<ReactDto>
            {
                Status = AppConstants.SuccessfulStatus,
                Succeeded = true,
                Entities = reactsDto
            };
        }

        public async Task<ListResponse<ReactDto>> GetReactsByUserIdAsync(string userId)
        {
            var reactsDto = await _context.Reacts
                .Where(r => r.UserId == userId)
                .Select(r => _mapper.Map<ReactDto>(r))
                .ToListAsync();
            return new ListResponse<ReactDto>
            {
                Status = AppConstants.SuccessfulStatus,
                Succeeded = true,
                Entities = reactsDto
            };
        }
    }
}
