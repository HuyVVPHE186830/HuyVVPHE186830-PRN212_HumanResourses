﻿using System;
using System.Collections.Generic;

namespace WpfApp.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Role Role { get; set; } = null!;
}
