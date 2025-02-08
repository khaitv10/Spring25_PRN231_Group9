using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class AppointmentService
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public DateTime? DoseDate { get; set; }

    public int? AppointmentId { get; set; }

    public int? ServiceId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Service? Service { get; set; }
}
