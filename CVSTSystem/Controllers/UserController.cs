﻿using BOs.RequestModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.UserServices;

namespace CVSTSystem.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("own")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetOwnUserInfo()
        {
            string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var response = await _userService.GetOwnUserInfo(token);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserInfoById(int id)
        {
            var response = await _userService.GetUserInfoById(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("own")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateOwnInfo(UserInfoUpdateModel model)
        {
            string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            await _userService.EditOwnInfo(token, model);
            return Ok("Update successfully");
        }

        [HttpPut]
        [Route("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] bool status)
        {
            await _userService.UpdateUserStatus(id, status);
            return Ok("Update successfully");
        }

        [HttpPost]
        [Route("staff")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStaff(StaffCreateModel model)
        {
            await _userService.CreateStaffAccount(model);
            return Ok("Create staff successfully");
        }
    }
}
