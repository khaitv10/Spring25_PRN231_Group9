using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace CVSTS_FE.Pages.Staff.StockManage
{
    public class DeleteModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public DeleteModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VaccineStock VaccineStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinestock = await _context.VaccineStocks.FirstOrDefaultAsync(m => m.Id == id);

            if (vaccinestock == null)
            {
                return NotFound();
            }
            else
            {
                VaccineStock = vaccinestock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinestock = await _context.VaccineStocks.FindAsync(id);
            if (vaccinestock != null)
            {
                VaccineStock = vaccinestock;
                _context.VaccineStocks.Remove(VaccineStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
