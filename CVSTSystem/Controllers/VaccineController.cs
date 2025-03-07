using BOs.Models;
using BOs.RequestModels.Vaccine;
using BOs.RequestModels.VaccineStock;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels.VaccineStock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Service.Service;
using Service.Service.VaccineServices;
using Service.Service.VaccineStockServices;
using System.Threading.Tasks;

namespace CVSTSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;
        private readonly IVaccineStockService _vaccineStockService;

        public VaccineController(IVaccineService vaccineService, IVaccineStockService vaccineStockService)
        {
            _vaccineService = vaccineService;
            _vaccineStockService = vaccineStockService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<VaccineInfoResponseModel>>> GetAllVaccines()
        {
            var vaccines = await _vaccineService.GetAllVaccines();
            return Ok(vaccines);
        }
        [HttpGet("active")]
        [Authorize(Roles = "Staff, User")]
        public async Task<ActionResult<List<VaccineInfoResponseModel>>> GetActiveVaccines()
        {
            var vaccines = await _vaccineService.GetActiveVaccines();
            return Ok(vaccines);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Staff, User")]
        public async Task<ActionResult<VaccineInfoResponseModel>> GetVaccineById(int id)
        {
            var vaccine = await _vaccineService.GetVaccineById(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            return Ok(vaccine);
        }

        [HttpGet("name/{name}")]
        [Authorize(Roles = "Staff, User")]
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
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult<VaccineCreateModel>> CreateVaccine(VaccineCreateModel vaccine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vaccineService.AddVaccine(vaccine);
            return Ok(new { Message = "Vaccine has been created", Vaccine = vaccine });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateVaccine(int id, VaccineInfoResponseModel vaccine)
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
        [Authorize(Roles = "Staff")]
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
        [Authorize(Roles = "Staff, User")]
        public async Task<ActionResult<bool>> CanVaccinate(int patientAge, int vaccineId)
        {
            var canVaccinate = await _vaccineService.CanVaccinate(patientAge, vaccineId);
            return Ok(canVaccinate);
        }
        [HttpGet("stock")]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult<List<VaccineStockResponseModel>>> GetAllVaccineStocks()
        {
            var vaccineStocks = await _vaccineStockService.GetAllVaccineStocks();
            return Ok(vaccineStocks);
        }

        [HttpGet("stock/{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult<VaccineStockResponseModel>> GetVaccineStockById(int id)
        {
            var vaccineStock = await _vaccineStockService.GetVaccineStockById(id);
            if (vaccineStock == null)
            {
                return NotFound();
            }
            return Ok(vaccineStock);
        }

        [HttpGet("stock/vaccine/{vaccineId}")]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult<List<VaccineStockResponseModel>>> GetVaccineStocksByVaccineId(int vaccineId)
        {
            var vaccineStocks = await _vaccineStockService.GetVaccineStocksByVaccineId(vaccineId);
            return Ok(vaccineStocks);
        }

        [HttpPost("stock")]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult<VaccineStockCreateModel>> CreateVaccineStock(VaccineStockCreateModel vaccineStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vaccineStockService.AddVaccineStock(vaccineStock);
            return Ok(new { Message = "Vaccine stock has been created", VaccineStock = vaccineStock });
        }

        [HttpPut("stock/{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateVaccineStock(int id, VaccineStockUpdateModel vaccineStock)
        {
            if (id != vaccineStock.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _vaccineStockService.UpdateVaccineStock(vaccineStock);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineStockExists(id))
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

        [HttpDelete("stock/{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteVaccineStock(int id)
        {
            var vaccineStock = await _vaccineStockService.GetVaccineStockById(id);
            if (vaccineStock == null)
            {
                return NotFound();
            }

            await _vaccineStockService.DeleteVaccineStock(id);
            return NoContent();
        }

        private bool VaccineStockExists(int id)
        {
            return _vaccineStockService.GetVaccineStockById(id) != null;
        }

    }
}
