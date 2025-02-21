//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using BOs.Models;
//using BOs.RequestModels.Service;

//namespace CVSTS_FE.Pages.Staff.ServiceManage
//{
//    public class CreateModel : PageModel
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public CreateModel(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }


//        public IActionResult OnGet()
//        {
//            return Page();
//        }

//        [BindProperty]
//        public ServiceCreateModel Service { get; set; } = default!;

//        // For more information, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            var client = _httpClientFactory.CreateClient("ApiClient");
//            var token = HttpContext.Session.GetString("JWToken");
//            if (!string.IsNullOrEmpty(token))
//            {
//                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
//            }
//            if (!string.IsNullOrEmpty(token))
//            {
//                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
//            }

//        }
//    }
//}
