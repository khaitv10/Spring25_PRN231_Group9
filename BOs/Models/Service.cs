using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? TotalDoses { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<DoseRecord> DoseRecords { get; set; } = new List<DoseRecord>();

    public virtual ICollection<DoseSchedule> DoseSchedules { get; set; } = new List<DoseSchedule>();

    public virtual ICollection<ServiceVaccine> ServiceVaccines { get; set; } = new List<ServiceVaccine>();
}
