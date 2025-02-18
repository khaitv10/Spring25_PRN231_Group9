using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace CVSTS_FE.Pages.DoseRecordManage
{
    public class EditModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public EditModel(BOs.Models.CvstsystemDbContext context)
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

            var doserecord =  await _context.DoseRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (doserecord == null)
            {
                return NotFound();
            }
            DoseRecord = doserecord;
           ViewData["ChildId"] = new SelectList(_context.Children, "Id", "FullName");
           ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
           ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DoseRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoseRecordExists(DoseRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DoseRecordExists(int id)
        {
            return _context.DoseRecords.Any(e => e.Id == id);
        }
    }
}
