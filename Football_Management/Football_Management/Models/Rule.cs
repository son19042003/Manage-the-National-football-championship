using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Rule
{
    public int RuleId { get; set; }

    public int MinAge { get; set; }

    public int MaxForeignPlayers { get; set; }

    public int MinPlayer { get; set; }

    public int MaxPlayer { get; set; }
}
