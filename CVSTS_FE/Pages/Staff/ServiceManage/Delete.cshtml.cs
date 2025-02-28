using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.Service;
using System.Net.Http.Headers;

namespace CVSTS_FE.Pages.Staff.ServiceManage
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public ServiceResponseModel Service { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"/api/Service/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            Service = await response.Content.ReadFromJsonAsync<ServiceResponseModel>();


            if (Service == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (Service == null || id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();

            // Send the update request to the API
            var response = await client.PutAsJsonAsync($"/api/Service/delete/{id}", Service);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error deleting service.");
                return Page();
            }

            var updatedServiceResponse = await client.GetAsync($"/api/Service/{id}");

            if (!updatedServiceResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error fetching updated service details.");
                return Page();
            }

            Service = await updatedServiceResponse.Content.ReadFromJsonAsync<ServiceResponseModel>();

            if (Service == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to fetch updated service details.");
                return Page();
            }

            ViewData["SuccessMessage"] = $"Service {id} updated successfully!";

            return Page();
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
