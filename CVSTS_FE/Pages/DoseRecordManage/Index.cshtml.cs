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
    public class IndexModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public IndexModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        public IList<DoseRecord> DoseRecord { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DoseRecord = await _context.DoseRecords
                .Include(d => d.Child)
                .Include(d => d.Service)
                .Include(d => d.Vaccine).ToListAsync();
        }
    }
}
