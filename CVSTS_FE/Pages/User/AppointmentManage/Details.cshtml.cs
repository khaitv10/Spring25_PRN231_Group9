using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.Child;
using System.Net.Http.Headers;
using BOs.ResponseModels.Appointment;
using Net.payOS.Types;
using System.Text.Json;
using System.Text;

namespace CVSTS_FE.Pages.User.AppointmentManage
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public AppointmentResModel Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid child ID.");
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"/getDetail/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            Appointment = await response.Content.ReadFromJsonAsync<AppointmentResModel>();


            if (Appointment == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var client = CreateAuthorizedClient();
            var responseAppointment = await client.GetAsync($"/getDetail/{id}");
            Appointment = await responseAppointment.Content.ReadFromJsonAsync<AppointmentResModel>();
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var paymentRequest = id;
            var paymentJson = new StringContent(JsonSerializer.Serialize(paymentRequest), Encoding.UTF8, "application/json");
            var paymentResponse = await client.PostAsync("/api/Payment/create-payment-link", paymentJson);

            if (paymentResponse.IsSuccessStatusCode)
            {
                var paymentJsonResponse = await paymentResponse.Content.ReadAsStringAsync();
                var paymentResult = JsonSerializer.Deserialize<CreatePaymentResult>(paymentJsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (paymentResult != null && !string.IsNullOrEmpty(paymentResult.checkoutUrl))
                {
                    return Redirect(paymentResult.checkoutUrl);
                }
            }

            ModelState.AddModelError("", "Failed to create payment link.");
            return Page();
        }

    }
}
