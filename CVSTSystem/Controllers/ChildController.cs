﻿using BOs.RequestModels.Child;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.ChildServices;

namespace CVSTSystem.Controllers
{
    [Route("api/child")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;

        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet("getAllChild")]
        public async Task<IActionResult> GetAllChild()
        {
            var response = await _childService.GetAllChilds();
            return Ok(response);
        }

        [HttpGet]
        [Route("/info/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetChildById(int id)
        {
            var response = await _childService.GetChildDetail(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("/detail/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetChildDetailById(int id)
        {
            var response = await _childService.GetDetailChild(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("/byParentId/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllChildByParentId(int id)
        {
            var response = await _childService.GetAllChildByParentId(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateChild(int id, ChildUpdateModel request)
        {
            await _childService.UpdateChild(id, request);
            return Ok("Update child successfully");
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            try
            {
                await _childService.DeleteChild(id);
                return Ok(new { message = "Delete child successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Can not delete: Child has appointments or dose schedules", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateChild([FromBody] ChildCreateModel request)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }

            await _childService.CreateChild(request, userId);
            return Ok("Create new child successfully");
        }
    }
}
