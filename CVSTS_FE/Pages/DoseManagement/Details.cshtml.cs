using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace CVSTS_FE.Pages.DoseManagement
{
    public class DetailsModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public DetailsModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        public DoseSchedule DoseSchedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doseschedule = await _context.DoseSchedules.FirstOrDefaultAsync(m => m.Id == id);
            if (doseschedule == null)
            {
                return NotFound();
            }
            else
            {
                DoseSchedule = doseschedule;
            }
            return Page();
        }
    }
}
