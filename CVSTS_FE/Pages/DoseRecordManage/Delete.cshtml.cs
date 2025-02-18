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
    public class DeleteModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public DeleteModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doserecord = await _context.DoseRecords.FindAsync(id);
            if (doserecord != null)
            {
                DoseRecord = doserecord;
                _context.DoseRecords.Remove(DoseRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
