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

        public UsersController(IUsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
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
            if (ModelState.IsValid)
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
            if (ModelState.IsValid)
            {
                var respone = _usersRepo.GetUsersByUsername(searchKey);
                return StatusCode(respone.Status, respone);
            }
            return BadRequest();
        }
    }
}
