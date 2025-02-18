using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;

namespace CVSTS_FE.Pages.DoseManagement
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
        ViewData["ChildId"] = new SelectList(_context.Children, "Id", "FullName");
        ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
        ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public DoseSchedule DoseSchedule { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DoseSchedules.Add(DoseSchedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
