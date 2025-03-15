using BOs.Models;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.PaymentServices
{
    public interface IPaymentService
    {
        Task<CreatePaymentResult> CreatPaymentLink(int appointId);
        Task<PaymentLinkInformation> GetPaymentInfor(long ordercode);
        Task CreatePayment(Payment payment);

    }
}
