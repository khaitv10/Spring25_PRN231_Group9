using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class VaccineStock
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int VaccineId { get; set; }

    public virtual Vaccine Vaccine { get; set; } = null!;
}
