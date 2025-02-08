using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Status { get; set; }

    public int? AppointmentId { get; set; }

    public int? ParentId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual User? Parent { get; set; }
}
