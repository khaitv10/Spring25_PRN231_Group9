using BOs.Models;
using BOs.RequestModels.Payment;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.AppointmentRepositories;
using Repository.Repositories.PaymentRepositories;
using Service.Service.PaymentServices;
using Service.Services.EmailServices;

namespace CVSTSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly HttpClient _httpClient;
        private readonly IPaymentRepository _paymentRepository;
       

        public PaymentController(IPaymentService paymentService, IAppointmentRepository appointmentRepository, HttpClient httpClient, IPaymentRepository paymentRepository, IEmailService emailService)
        {
            _paymentService = paymentService;
            _appointmentRepository = appointmentRepository;
            _httpClient = httpClient;
            _paymentRepository = paymentRepository;
            
        }

        [HttpPost("create-payment-link")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] int appointmentId)
        {
            try
            {
                var paymentResult = await _paymentService.CreatPaymentLink(appointmentId);

                // Trả về `paymentLinkId` và `checkoutUrl`
                return Ok(new { paymentLink = paymentResult.checkoutUrl, paymentLinkId = paymentResult.paymentLinkId, orderCode = paymentResult.orderCode });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("payment/callback")]
        public async Task<IActionResult> PaymentCallback([FromBody] PaymentCallbackRequest callbackRequest)
        {
            int appointmentId = callbackRequest.AppointmentId;
            long ordercode = callbackRequest.OrderCode;

            // Kiểm tra trạng thái thanh toán từ PayOS
            var paymentInfor = await _paymentService.GetPaymentInfor(ordercode);
            var payment = new Payment
            {
                AppointmentId = appointmentId,
                PaymentMethod = "PayOs",
                TransactionCode = paymentInfor.transactions[0].reference,
                PaidAt = DateTime.UtcNow,
                PaymentStatus = paymentInfor.status,
                Amount = paymentInfor.amount,
                
            };
            await _paymentService.CreatePayment(payment);
            if (paymentInfor.status == "success")
            {
                var appointment = await _appointmentRepository.GetByID(appointmentId);
                if (appointment == null)
                {
                    return NotFound($"Appointment does not exist");
                }
                appointment.Status = "Paid";
                await _appointmentRepository.Update(appointment);

                return Ok("Payment successfully");
            }

            return BadRequest("Payment failed");
        }
        [HttpGet("payment-result")]
        public IActionResult PaymentResult([FromQuery] int id, [FromQuery] string status)
        {
            if (status == "CANCELLED")
            {
                // Xử lý logic cho hủy thanh toán
                return Ok(new { message = "Payment canceled", id, status });
            }
            else
            {
                // Xử lý logic cho thanh toán thành công
                return Ok(new { message = "Payment successfully", id, status });
            }
        }
        [HttpGet("success")]
        public async Task<IActionResult> PaymentSuccess([FromQuery] string id, [FromQuery] string status)
        {
            // Log the PaymentLinkId value
            Console.WriteLine($"Received PaymentLinkId: {id}");

            // Check if payment was successful
            if (status != "PAID")
            {
                return BadRequest(new { message = "Payment status is not available", id, status });
            }

            // Retrieve the payment information using the PaymentLinkId
            var payment = await _paymentRepository.GetSingle(x => x.TransactionCode == id);
            payment.TransactionCode = id;
            payment.PaymentStatus = status;
            payment.PaidAt = DateTime.Now;
            await _paymentRepository.Update(payment);
            if (payment == null)
            {
                return NotFound(new { message = "Can not find apointment with this transactioncode" });
            }

            // Retrieve the appointment details using AppointmentId from payment
            var appointment = await _appointmentRepository.GetByID((int)payment.AppointmentId);
            if (appointment == null)
            {
                return NotFound(new { message = "Appointment does not exist" });
            }

            // Update the appointment status 
            appointment.PaymentStatus = "paid";
            await _appointmentRepository.Update(appointment);

           

            return Ok(new { message = "Payment successfully.", id, status });
        }

        [HttpGet("cancel")]
        public async Task<IActionResult> CancelPayment(
               [FromQuery] string id,
               [FromQuery] string status,
               [FromQuery] string code,
               [FromQuery] string cancel,
               [FromQuery] string orderCode)
        {
            // Log the PaymentLinkId value
            Console.WriteLine($"Received PaymentLinkId: {id}");

            // Retrieve payment information using the PaymentLinkId
            var payment = await _paymentRepository.GetSingle(x => x.TransactionCode == id);

            if (payment == null)
            {
                return NotFound(new { message = "Không tìm thấy thanh toán với PaymentLinkId này" });
            }

            // Update the appointment status to 1 if payment was cancelled
            if (status == "CANCELLED")
            {
               
                // Send email notification
                var appointment = await _appointmentRepository.GetByID((int)payment.AppointmentId);
                payment.PaymentStatus = "canceled";
                payment.TransactionCode = id;
                payment.PaidAt = DateTime.Now;
                await _paymentRepository.Update(payment);
                if (appointment != null)
                {
                    appointment.PaymentStatus = "canceled";
                    await _appointmentRepository.Update(appointment);
                }

                return Ok(new { message = "Paymnet has been canceled", id, status });
            }

            return BadRequest(new { message = "Yêu cầu không hợp lệ" });
        }

    }
}
