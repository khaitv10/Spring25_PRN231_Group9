using BOs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.IService;
using Service.Service; 
using System.Threading.Tasks;

namespace CVSTSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService; 

        public VaccineController(IVaccineService vaccineService) 
        {
            _vaccineService = vaccineService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vaccine>>> GetAllVaccines()
        {
            var vaccines = await _vaccineService.GetAllVaccines();
            return Ok(vaccines); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccine>> GetVaccineById(int id)
        {
            var vaccine = await _vaccineService.GetVaccineById(id);
            if (vaccine == null)
            {
                return NotFound(); 
            }
            return Ok(vaccine);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Vaccine>> GetVaccineByName(string name)
        {
            var vaccine = await _vaccineService.GetVaccineByName(name);
            if (vaccine == null)
            {
                return NotFound();
            }
            return Ok(vaccine);
        }

        [HttpPost]
        public async Task<ActionResult<Vaccine>> CreateVaccine(Vaccine vaccine)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }

            await _vaccineService.AddVaccine(vaccine);
            return Ok(new { Message = "Vaccine has been created", Vaccine = vaccine }); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccine(int id, Vaccine vaccine)
        {
            if (id != vaccine.Id)
            {
                return BadRequest(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _vaccineService.UpdateVaccine(vaccine);
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!VaccineExists(id))
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
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            var vaccine = await _vaccineService.GetVaccineById(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            await _vaccineService.DeleteVaccine(id);
            return NoContent(); 
        }

        private bool VaccineExists(int id)
        {
            return _vaccineService.GetVaccineById(id) != null;
        }

        
        [HttpGet("canvaccinate/{patientAge}/{vaccineId}")]
        public async Task<ActionResult<bool>> CanVaccinate(int patientAge, int vaccineId)
        {
            var canVaccinate = await _vaccineService.CanVaccinate(patientAge, vaccineId);
            return Ok(canVaccinate);
        }
    }
}