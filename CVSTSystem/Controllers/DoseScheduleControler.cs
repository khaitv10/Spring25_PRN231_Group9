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
    [Route("api/[controller]")]
    [ApiController]
    public class DoseScheduleControler : ODataController
    {
        private readonly IDoseScheduleService _doseScheduleService;
        private readonly IDoseRecordService _doseRecordService;

        public DoseScheduleControler(IDoseScheduleService doseScheduleservice, IDoseRecordService doseRecordservice)
        {
            _doseScheduleService = doseScheduleservice;
            _doseRecordService = doseRecordservice;
        }

        [HttpGet("schedule")]
        public async Task<ActionResult<List<DoseScheduleResponseModel>>> GetAllDoseSchedule()
        {
            var doseSchedules = await _doseScheduleService.GetAllDoseSchedule();
            return Ok(doseSchedules);
        }

        [HttpGet("schedule/{id}")]
        public async Task<ActionResult<DoseScheduleResponseModel>> GetDoseScheduleById(int id)
        {
            var doseSchedule = await _doseScheduleService.GetDoseScheduleById(id);
            if (doseSchedule == null) {return NotFound();}
            return Ok(doseSchedule);
        }

        [HttpPost("schedule")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<DoseScheduleCreateModel>> CreateVaccine(DoseScheduleCreateModel doseSchedule)
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

        [HttpPut("schedule/{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<IActionResult> UpdateDoseSchedule(int id, DoseScheduleUpdateModel doseSchedule)
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

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            var vaccine = await _doseScheduleService.GetDoseScheduleById(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            await _doseScheduleService.DeleteDoseSchedule(id);
            return NoContent();
        }

        private bool VaccineExists(int id)
        {
            return _doseScheduleService.GetDoseScheduleById(id) != null;
        }
    }
}
