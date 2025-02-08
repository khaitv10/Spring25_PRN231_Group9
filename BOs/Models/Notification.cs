using System;
using System.Collections.Generic;

namespace BOs.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
