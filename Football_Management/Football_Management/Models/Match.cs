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

    public string? TypeGoal { get; set; }

    public string Status { get; set; } = null!;

    public int? PlayerId { get; set; }

    public int? TypeGoalH { get; set; }

    public int? TypeGoalA { get; set; }

    public string? TimeGoal { get; set; }

    public virtual Club AwayTeamNavigation { get; set; } = null!;

    public virtual Club HomeTeamNavigation { get; set; } = null!;

    public virtual Player? Player { get; set; }

    public virtual TypeGoal? TypeGoalANavigation { get; set; }

    public virtual TypeGoal? TypeGoalHNavigation { get; set; }
}
