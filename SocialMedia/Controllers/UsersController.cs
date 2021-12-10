﻿using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register(UserDto userDto) {
            var response = await _usersRepo.RegisterAsync(userDto);
            return StatusCode(response.Status,response);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _usersRepo.LoginAsync(loginDto);
            return StatusCode(response.Status, response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id,UserDto userDto)
        {
            var response = await _usersRepo.UpdateAsync(id,userDto);
            return StatusCode(response.Status, response);
        }
    }
}
