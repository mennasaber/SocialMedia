using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Data.Dtos;
using SocialMedia.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepo _postsRepo;
        private readonly IReactsRepo _reactsRepo;

        public PostsController(IPostsRepo postsRepo, IReactsRepo reactsRepo)
        {
            _postsRepo = postsRepo;
            _reactsRepo = reactsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPost(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _postsRepo.GetPostByIdAsync(id);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(BasePostDto basePostDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _postsRepo.AddPostAsync(basePostDto);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePostAsync(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _postsRepo.DeletePostAsync(id);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePostAsync(int id, string text)
        {
            if (ModelState.IsValid)
            {
                var response = await _postsRepo.UpdateAsync(id, text);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        
        [HttpGet]
        [Route("reacts")]
        public async Task<IActionResult> GetReacts(int postId)
        {
            if (ModelState.IsValid)
            {
                var response = await _reactsRepo.GetReactsByPostIdAsync(postId);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
    }
}
