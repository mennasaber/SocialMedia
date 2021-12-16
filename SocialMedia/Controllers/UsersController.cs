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
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _usersRepo;
        private readonly IPostsRepo _postRepo;
        private readonly IReactsRepo _reactsRepo;

        public UsersController(IUsersRepo usersRepo, IPostsRepo postRepo, IReactsRepo reactsRepo)
        {
            _usersRepo = usersRepo;
            _postRepo = postRepo;
            _reactsRepo = reactsRepo;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _usersRepo.RegisterAsync(registerDto);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _usersRepo.LoginAsync(loginDto);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, UserDto userDto)
        {
            if (ModelState.IsValid && id != null && !id.Trim().Equals(""))
            {
                var response = await _usersRepo.UpdateAsync(id, userDto);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("Search")]
        public IActionResult SearchByUsername(string searchKey)
        {
            if (searchKey != null && !searchKey.Trim().Equals(""))
            {
                var respone = _usersRepo.GetUsersByUsername(searchKey);
                return StatusCode(respone.Status, respone);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            if (ModelState.IsValid)
            {
                var reponse = await _usersRepo.GetUserByIdAsync(id);
                return StatusCode(reponse.Status, reponse);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("posts")]
        public IActionResult GetUserPosts(string id)
        {
            if (ModelState.IsValid)
            {
                var response = _postRepo.GetPostsForUser(id);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("reacts")]
        public async Task<IActionResult> GetReacts(string userId)
        {
            if (ModelState.IsValid)
            {
                var response = await _reactsRepo.GetReactsByUserIdAsync(userId);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("reacts")]
        public async Task<IActionResult> AddReact(BaseReactDto baseReactDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _reactsRepo.AddReactAsync(baseReactDto);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("reacts")]
        public async Task<IActionResult> DeleteReact(int reactId)
        {
            if (ModelState.IsValid)
            {
                var response = await _reactsRepo.DeleteReactAsync(reactId);
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
    }
}
