using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Match
{
    public int MatchId { get; set; }

    public int Round { get; set; }

    public TimeOnly TimeStart { get; set; }

    public DateOnly DateStart { get; set; }

    public string HomeTeam { get; set; } = null!;

    public string AwayTeam { get; set; } = null!;

    public int? GoalsH { get; set; }

    public int? GoalsA { get; set; }

    public string Status { get; set; } = null!;

    public virtual Club AwayTeamNavigation { get; set; } = null!;

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    public virtual Club HomeTeamNavigation { get; set; } = null!;
}
