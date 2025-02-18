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

       
    }
}
