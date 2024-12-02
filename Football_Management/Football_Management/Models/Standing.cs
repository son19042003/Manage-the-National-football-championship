using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Standing
{
    public int StandingId { get; set; }

    public string ClubId { get; set; } = null!;

    public int? Played { get; set; }

    public int? Won { get; set; }

    public int? Drawn { get; set; }

    public int? Lost { get; set; }

    public int? GoalF { get; set; }

    public int? GoalA { get; set; }

    public int? Points { get; set; }

    public virtual Club Club { get; set; } = null!;
}
