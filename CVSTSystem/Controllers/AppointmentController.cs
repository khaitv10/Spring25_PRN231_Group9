using BOs.RequestModels.Appointment;
using BOs.RequestModels.Child;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.AppointmentServices;

namespace CVSTSystem.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentServices _appService;

        public AppointmentController(IAppointmentServices appService)
        {
            _appService = appService;
        }

        [HttpGet("getAllAppointment")]
        public async Task<IActionResult> getAllAppointment()
        {
            var response = await _appService.GetAllAppointments();
            return Ok(response);
        }

  
        [HttpGet]
        [Route("/getDetail/{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var response = await _appService.GetAppointmentDetail(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("/getByParentId/{id}")]
        public async Task<IActionResult> GetAllByParentId(int id)
        {
            var response = await _appService.GetAllByParentId(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointCreateModel request)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized("User is not authenticated.");
            }

            await _appService.CreateAppointment(request, userId);
            return Ok("Create new appointment successfully");
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateAppointmentStatus(int id, string status)
        {
            var result = await _appService.UpdateAppointmentStatus(id, status);
            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }
            return Ok("Update appointment status successfully"); 
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var result = await _appService.DeleteAppointment(id, userId);
            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }
            return Ok("Delete appointment successfully");
        }

    }
}
