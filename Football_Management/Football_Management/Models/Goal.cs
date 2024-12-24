using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Goal
{
    public int GoalId { get; set; }

    public int MatchId { get; set; }

    public int PlayerId { get; set; }

    public string TeamId { get; set; } = null!;

    public string TimeScored { get; set; } = null!;

    public int TypeGid { get; set; }

    public int GoalIndex { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;

    public virtual Club Team { get; set; } = null!;

    public virtual TypeGoal TypeG { get; set; } = null!;
}
