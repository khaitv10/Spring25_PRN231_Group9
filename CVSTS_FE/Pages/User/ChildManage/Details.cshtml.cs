using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace CVSTS_FE.Pages.User.ChildManage
{
    public class DetailsModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public DetailsModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        public Child Child { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.FirstOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }
            else
            {
                Child = child;
            }
            return Page();
        }
    }
}
