using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class TypeGoal
{
    public int TypeGid { get; set; }

    public string? TypeGname { get; set; }

    public string? TypeGdes { get; set; }

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
}
