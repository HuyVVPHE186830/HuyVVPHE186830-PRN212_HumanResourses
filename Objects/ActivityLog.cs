using System;
using System.Collections.Generic;

namespace WpfApp.Models;

public partial class ActivityLog
{
    public int ActivityLogId { get; set; }

    public int AccountId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual Account Account { get; set; } = null!;
}
