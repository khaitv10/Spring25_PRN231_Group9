using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace CVSTS_FE.Pages.DoseRecordManage
{
    public class DetailsModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public DetailsModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        public DoseRecord DoseRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doserecord = await _context.DoseRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (doserecord == null)
            {
                return NotFound();
            }
            else
            {
                DoseRecord = doserecord;
            }
            return Page();
        }
    }
}
