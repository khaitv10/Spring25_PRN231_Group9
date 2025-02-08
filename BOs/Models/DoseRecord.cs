using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class DoseRecord
{
    public int Id { get; set; }

    public DateOnly DoseDate { get; set; }

    public int? DoseNumber { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public int? ServiceId { get; set; }

    public int? ChildId { get; set; }

    public int? VaccineId { get; set; }

    public virtual Child? Child { get; set; }

    public virtual Service? Service { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}
