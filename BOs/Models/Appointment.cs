using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? Type { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public string? PaymentStatus { get; set; }

    public int? ServiceId { get; set; }

    public int? ParentId { get; set; }

    public int? ChildId { get; set; }

    public virtual ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();

    public virtual Child? Child { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User? Parent { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Service? Service { get; set; }
}
