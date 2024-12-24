using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Club
{
    public string ClubId { get; set; } = null!;

    public string ClubName { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public string Stadium { get; set; } = null!;

    public string? Logo { get; set; }

    public string? LinkFb { get; set; }

    public string? LinkIg { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    public virtual ICollection<Match> MatchAwayTeamNavigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHomeTeamNavigations { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Standing> Standings { get; set; } = new List<Standing>();
}
