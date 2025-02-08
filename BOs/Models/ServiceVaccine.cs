using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class ServiceVaccine
{
    public int Id { get; set; }

    public int? NumberOfDose { get; set; }

    public int? VaccineId { get; set; }

    public int? ServiceId { get; set; }

    public virtual Service? Service { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}
