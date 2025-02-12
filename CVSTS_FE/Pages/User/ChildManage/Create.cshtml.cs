using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;

namespace CVSTS_FE.Pages.User.ChildManage
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
        ViewData["ParentId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Child Child { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Children.Add(Child);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
