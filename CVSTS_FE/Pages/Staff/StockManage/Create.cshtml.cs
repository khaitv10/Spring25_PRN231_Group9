using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;

namespace CVSTS_FE.Pages.Staff.StockManage
{
    public class CreateModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public CreateModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public VaccineStock VaccineStock { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.VaccineStocks.Add(VaccineStock);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
