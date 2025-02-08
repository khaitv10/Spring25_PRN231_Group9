using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class Payment
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? PaymentStatus { get; set; }

    public DateTime? PaidAt { get; set; }

    public string? TransactionCode { get; set; }

    public int? AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
