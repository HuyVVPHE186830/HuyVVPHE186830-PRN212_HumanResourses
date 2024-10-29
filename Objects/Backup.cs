using System;
using System.Collections.Generic;

namespace Objects;

public partial class Backup
{
    public int BackupId { get; set; }

    public DateTime BackupDate { get; set; }

    public string BackupFile { get; set; } = null!;
}
