using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;

namespace CVSTS_FE.Pages.VaccineManage
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
            return Page();
        }

        [BindProperty]
        public Vaccine Vaccine { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Vaccines.Add(Vaccine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
