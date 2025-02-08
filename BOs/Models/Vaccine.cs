using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class Vaccine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Origin { get; set; }

    public int? MinAge { get; set; }

    public int? MaxAge { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<DoseRecord> DoseRecords { get; set; } = new List<DoseRecord>();

    public virtual ICollection<DoseSchedule> DoseSchedules { get; set; } = new List<DoseSchedule>();

    public virtual ICollection<ServiceVaccine> ServiceVaccines { get; set; } = new List<ServiceVaccine>();

    public virtual ICollection<VaccineStock> VaccineStocks { get; set; } = new List<VaccineStock>();
}
