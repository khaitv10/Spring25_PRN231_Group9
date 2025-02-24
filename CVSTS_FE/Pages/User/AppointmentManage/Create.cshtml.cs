using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;
using BOs.ResponseModels.Appointment;
using BOs.RequestModels.Appointment;
using System.Text.Json;
using System.Text;

namespace CVSTS_FE.Pages.User.AppointmentManage
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [BindProperty]
        public AppointCreateModel Appointment { get; set; } = new AppointCreateModel();
        public List<ChildResponse> Children { get; set; } = new List<ChildResponse>();
        public List<ServiceResponse> Services { get; set; } = new List<ServiceResponse>();
        public List<int> SelectedServiceIds { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadChildrenAndServices();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadChildrenAndServices(); 
                return Page();
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToPage("/Login");
            }

            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            Appointment.ParentId = userId;
            var jsonContent = new StringContent(JsonSerializer.Serialize(Appointment), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/appointment", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to create appointment.");
                await LoadChildrenAndServices(); 
                return Page();
            }
        }

        private async Task LoadChildrenAndServices()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return;
            }

            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var childResponse = await client.GetAsync($"/byParentId/{userId}");
            if (childResponse.IsSuccessStatusCode)
            {
                var childJson = await childResponse.Content.ReadAsStringAsync();
                Children = JsonSerializer.Deserialize<List<ChildResponse>>(childJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ChildResponse>();
            }

            var serviceResponse = await client.GetAsync("/api/service/getAllService");
            if (serviceResponse.IsSuccessStatusCode)
            {
                var serviceJson = await serviceResponse.Content.ReadAsStringAsync();
                Services = JsonSerializer.Deserialize<List<ServiceResponse>>(serviceJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ServiceResponse>();
            }
        }

    }
}
