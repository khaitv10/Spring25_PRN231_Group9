using BOs.Models;
using BOs.RequestModels.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Service.ServiceService;

namespace CVSTSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("getAllService")]
        public async Task<IActionResult> GetAllService([FromQuery]ServiceQueryModel query)
        {
            var response = await _serviceService.GetAllService(query);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var response = await _serviceService.GetServiceById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateServiceStatus(int id)
        {
            await _serviceService.UpateServiceStatus(id);
            return Ok("Update successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody]ServiceCreateModel model)
        {
            var result = await _serviceService.CreateService(model);
            if(result.Success == false)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetServiceById), new { id = result.Data.Id }, result.Data); ;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService([FromBody] ServiceUpdateModel model, int id)
        {
            var result = await _serviceService.UpdateService(id, model);
            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
