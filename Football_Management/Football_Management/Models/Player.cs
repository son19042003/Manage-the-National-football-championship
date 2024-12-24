using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public DateOnly Birthday { get; set; }

    public double Height { get; set; }

    public string Nationality { get; set; } = null!;

    public bool? TypePlayer { get; set; }

    public string Position { get; set; } = null!;

    public int Number { get; set; }

    public int? Goals { get; set; }

    public string? Avatar { get; set; }

    public string? LinkFb { get; set; }

    public string? LinkIg { get; set; }

    public string ClubId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool IsInClub { get; set; }

    public virtual Club Club { get; set; } = null!;

    public virtual ICollection<Goal> GoalsNavigation { get; set; } = new List<Goal>();
}
