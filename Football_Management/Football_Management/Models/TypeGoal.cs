using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class TypeGoal
{
    public int TypeGid { get; set; }

    public string? TypeGname { get; set; }

    public string? TypeGdes { get; set; }

    public virtual ICollection<Match> MatchTypeGoalANavigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchTypeGoalHNavigations { get; set; } = new List<Match>();
}
