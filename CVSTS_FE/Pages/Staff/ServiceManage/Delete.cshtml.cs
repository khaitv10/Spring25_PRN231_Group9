﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using BOs.Models;

//namespace CVSTS_FE.Pages.Staff.ServiceManage
//{
//    public class DeleteModel : PageModel
//    {
//        private readonly BOs.Models.CvstsystemDbContext _context;

//        public DeleteModel(BOs.Models.CvstsystemDbContext context)
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

//            var service = await _context.Services.FirstOrDefaultAsync(m => m.Id == id);

//            if (service == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                Service = service;
//            }
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var service = await _context.Services.FindAsync(id);
//            if (service != null)
//            {
//                Service = service;
//                _context.Services.Remove(Service);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToPage("./Index");
//        }
//    }
//}
