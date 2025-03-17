using BOs.Models;
using BOs.RequestModels.Payment;
using BOs.ResponseModels.Appointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Utilities;
using System.Text;
using System.Text.Json;

namespace CVSTS_FE.Pages.User
{
    public class PaymentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PaymentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }
        [BindProperty(SupportsGet = true)]
        public int AppointId { get; set; } = default!;
        [BindProperty(SupportsGet = true)]

        public string Status { get; set; }
        [BindProperty(SupportsGet = true)]

        public long OrderCode { get; set; }
        public async Task<IActionResult> OnGetAsync(int appointId,string status, long orderCode)
        {
            

           
            using var client = _httpClientFactory.CreateClient();

            var paymentRequest = new PaymentCallbackRequest {
            
            AppointmentId = appointId,
            OrderCode= orderCode,
            };
            var paymentJson = new StringContent(JsonSerializer.Serialize(paymentRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync("https://localhost:7168/api/Payment/payment/callback", paymentRequest);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(response.ReasonPhrase);
            }

            return Page();
        }
    }
}
