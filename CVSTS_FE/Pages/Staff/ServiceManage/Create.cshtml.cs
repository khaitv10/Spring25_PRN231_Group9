using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;
using BOs.RequestModels.Service;
using System.Net.Http.Headers;
using BOs.ResponseModels.Vaccine;

namespace CVSTS_FE.Pages.Staff.ServiceManage
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var client = CreateAuthorizedClient();
            var vaccineResponse = await client.GetAsync($"/api/Vaccine");
            var vaccine = await vaccineResponse.Content.ReadFromJsonAsync<List<VaccineInfoResponseModel>>();

            ViewData["Vaccine"] = new SelectList(vaccine, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public ServiceCreateModel Service { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            // Debugging: Check if Vaccines are being populated
            foreach (var vaccine in Service.Vaccines)
            {
                if (vaccine.Value <= 0)
                {
                    ModelState.AddModelError("", "Number of doses must be positive.");
                    return Page();
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CreateAuthorizedClient();
            var response = await client.PostAsJsonAsync("/api/Service", Service);

            if (response.IsSuccessStatusCode)
            {
                ViewData["SuccessMessage"] = "Service created successfully!";
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the service.");
                return Page();
            }
        }

        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");


            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
