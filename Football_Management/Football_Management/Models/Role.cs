﻿using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? RoleDes { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}