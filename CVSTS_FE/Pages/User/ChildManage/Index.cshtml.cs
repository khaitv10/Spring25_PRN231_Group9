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
    public class IndexModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public IndexModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        public IList<Child> Child { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Child = await _context.Children
                .Include(c => c.Parent).ToListAsync();
        }
    }
}
