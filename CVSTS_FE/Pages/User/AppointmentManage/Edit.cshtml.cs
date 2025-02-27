using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.Appointment;
using BOs.RequestModels.Appointment;
using System.Text.Json;
using System.Text;

namespace CVSTS_FE.Pages.User.AppointmentManage
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AppointUpdateModel Appointment { get; set; } = default!;
        public List<ChildResponse> Children { get; set; } = new List<ChildResponse>();
        public List<ServiceResponse> Services { get; set; } = new List<ServiceResponse>();
        public List<int> SelectedServiceIds { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadChildrenAndServices(id);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                await LoadChildrenAndServices(id);
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

            var jsonContent = new StringContent(JsonSerializer.Serialize(Appointment), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/appointment/info/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update appointment.");
                await LoadChildrenAndServices(id);
                return Page();
            }
        }

        private async Task LoadChildrenAndServices(int id)
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
            var appointResponse = await client.GetAsync($"/getdetail/{id}");
            if (appointResponse.IsSuccessStatusCode) 
            {
                var appointJson = await appointResponse.Content.ReadAsStringAsync();
                 var appointment = JsonSerializer.Deserialize<AppointmentResModel>(appointJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new AppointmentResModel();
                Appointment = new AppointUpdateModel()
                {
                    AppointmentDate = appointment.AppointmentDate,
                    ChildId = appointment.Child.Id,
                    SelectedServiceIds = appointment.Services.Select(x => x.Id).ToList(),
                    PaymentStatus = appointment.PaymentStatus
                };
                

            }
        }

    }
}
