using BOs.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.PaymentRepositories
{
    public class PaymentRepository : GenericDAO<Payment>, IPaymentRepository
    {
    }
}
