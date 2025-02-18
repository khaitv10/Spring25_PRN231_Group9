using BOs.Models;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.DoseSchedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Service.Service.DoseRecordServices;
using Service.Service.DoseScheduleServices;

namespace CVSTSystem.Controllers
{
    [Route("api/doseSchedule")]
    [ApiController]
    public class DoseScheduleControler : Controller
    {
        private readonly IDoseScheduleService _doseScheduleService;
        

        public DoseScheduleControler(IDoseScheduleService doseScheduleservice, IDoseRecordService doseRecordservice)
        {
            _doseScheduleService = doseScheduleservice;
            
        }

        [HttpGet("dose_schedules")]
        // [Authorize(Roles = "Staff")]
        public async Task<ActionResult<List<DoseScheduleResponseModel>>> GetAllDoseSchedule()
        {
            var doseSchedules = await _doseScheduleService.GetAllDoseSchedule();
            return Ok(doseSchedules);
        }

        [HttpGet]
        [Route("schedule/{id}")]
        // [Authorize(Roles = "Staff")]
        public async Task<ActionResult<DoseScheduleResponseModel>> GetDoseScheduleById(int id)
        {
            var doseSchedule = await _doseScheduleService.GetDoseScheduleById(id);
            if (doseSchedule == null) {return NotFound();}
            return Ok(doseSchedule);
        }

        [HttpPost]
        [Route("add")]
       //[Authorize(Roles = "Staff")]

        public async Task<ActionResult<DoseScheduleCreateModel>> CreateVaccine([FromBody] DoseScheduleCreateModel doseSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _doseScheduleService.AddDoseSchedule(doseSchedule);
            return Ok(new { Message = "Vaccine has been created", DoseSchedule = doseSchedule });
        }

        private bool DoseScheduleExists(int id)
        {
            return _doseScheduleService.GetDoseScheduleById(id) != null;
        }

        [HttpPut]
        [Route("{id}")]
        //[Authorize(Roles = "Staff")]

        public async Task<ActionResult<DoseScheduleUpdateModel>> UpdateDoseSchedule(int id, DoseScheduleUpdateModel doseSchedule)
        {
            if (id != doseSchedule.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _doseScheduleService.UpdateDoseSchedule(doseSchedule);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoseScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Update dose schedule successfully");
        }

        [HttpDelete]
        [Route("schedule/{id}")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            

            await _doseScheduleService.DeleteDoseSchedule(id);
            return Ok("Delete dose schedule successfully");
        }

        
    }
}
