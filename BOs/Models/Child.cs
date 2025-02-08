using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class Child
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string? Gender { get; set; }

    public string? Note { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<DoseRecord> DoseRecords { get; set; } = new List<DoseRecord>();

    public virtual ICollection<DoseSchedule> DoseSchedules { get; set; } = new List<DoseSchedule>();

    public virtual User? Parent { get; set; }
}
