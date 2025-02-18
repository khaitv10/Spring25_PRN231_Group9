using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;

namespace CVSTS_FE.Pages.DoseManagement
{
    public class EditModel : PageModel
    {
        private readonly BOs.Models.CvstsystemDbContext _context;

        public EditModel(BOs.Models.CvstsystemDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoseSchedule DoseSchedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doseschedule =  await _context.DoseSchedules.FirstOrDefaultAsync(m => m.Id == id);
            if (doseschedule == null)
            {
                return NotFound();
            }
            DoseSchedule = doseschedule;
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

            _context.Attach(DoseSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoseScheduleExists(DoseSchedule.Id))
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

        private bool DoseScheduleExists(int id)
        {
            return _context.DoseSchedules.Any(e => e.Id == id);
        }
    }
}
