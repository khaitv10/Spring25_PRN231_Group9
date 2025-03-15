using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BOs.Models;
using System.Net.Http.Headers;

namespace CVSTS_FE.Pages.User.ChildManage
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Child Child { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid child ID.");
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"/info/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            Child = await response.Content.ReadFromJsonAsync<Child>();

            if (Child == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Child == null || Child.Id == 0)
            {
                return NotFound();
            }

            var id = Child.Id;
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"/api/child/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                if (errorContent.Contains("Can not delete"))
                {
                    ModelState.AddModelError("DeleteError", "Cannot delete: Child has appointments or dose schedules.");
                }
                else
                {
                    ModelState.AddModelError("DeleteError", "An error occurred while deleting the child.");
                }

                var existingChild = await client.GetFromJsonAsync<Child>($"/info/{id}");
                if (existingChild != null)
                {
                    Child = existingChild;
                }

                return Page(); 
            }

            return RedirectToPage("./Index");
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
