using BOs.Models;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json;
using Repository.Repositories.AppointmentRepositories;
using Repository.Repositories.PaymentRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly PayOS _payOS;
        private readonly HttpClient _httpClient;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IConfiguration configuration, IAppointmentRepository appointmentRepository, HttpClient httpClient, IPaymentRepository paymentRepository)
        {
            _payOS = new PayOS(
                configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment")
            );
            _appointmentRepository = appointmentRepository;
            _httpClient = httpClient;
            _paymentRepository = paymentRepository;
        }

        public async Task CreatePayment(Payment payment)
        {
            await _paymentRepository.Insert(payment);
        }

        public async Task<CreatePaymentResult> CreatPaymentLink(int appointId)
        {
            var appointment = await _appointmentRepository.GetByID(appointId);
            if (appointment == null)
            {
                throw new Exception("Appointment does not exist");
            }

            long orderCode = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            ItemData item = new ItemData($"Appointment {appointment.Id.ToString()}", 1, (int)appointment.TotalPrice);
            List<ItemData> items = new List<ItemData> { item };
            string description = "Thanh";

            PaymentData paymentData = new PaymentData(
                 orderCode,
                 (int)appointment.TotalPrice,
                 description,
                 items,
                 "https://www.nyan.cat/",
                 "https://asoftmurmur.com/"
            );

            // Gửi yêu cầu tạo liên kết thanh toán
            var paymentResult = await _payOS.createPaymentLink(paymentData);

            // Lưu `paymentLinkId` vào Database (để dùng trong callback)
            var paymentRecord = new Payment
            {
               Amount = (decimal)appointment.TotalPrice,
               PaymentMethod = "PayOs",
               PaidAt = DateTime.Now,
               AppointmentId = appointment.Id,
            };
           
            await _paymentRepository.Insert(paymentRecord);

            // Tạo một đối tượng mới với `paymentLinkId` được khởi tạo
            var updatedPaymentResult = paymentResult with { orderCode = paymentResult.orderCode, paymentLinkId = paymentResult.paymentLinkId,  };

            return updatedPaymentResult;

        }

        public async Task<PaymentLinkInformation> GetPaymentInfor(long ordercode)
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(ordercode);
            return paymentLinkInformation; // Trả về trạng thái thanh toán (e.g., "success", "pending", ...)
        }
        public class PaymentStatusResponse
        {
            [JsonProperty("status")]
            public string Status { get; set; }
        }
    }
}
