//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using BOs.Models;

//namespace CVSTS_FE.Pages.Staff.ServiceManage
//{
//    public class EditModel : PageModel
//    {
//        private readonly BOs.Models.CvstsystemDbContext _context;

//        public EditModel(BOs.Models.CvstsystemDbContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public Service Service { get; set; } = default!;

//        public async Task<IActionResult> OnGetAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var service =  await _context.Services.FirstOrDefaultAsync(m => m.Id == id);
//            if (service == null)
//            {
//                return NotFound();
//            }
//            Service = service;
//            return Page();
//        }

//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more information, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            _context.Attach(Service).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ServiceExists(Service.Id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return RedirectToPage("./Index");
//        }

//        private bool ServiceExists(int id)
//        {
//            return _context.Services.Any(e => e.Id == id);
//        }
//    }
//}
