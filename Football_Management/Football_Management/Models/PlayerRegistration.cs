using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class PlayerRegistration
{
    public int PlayerRegisId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public double Height { get; set; }

    public string Nationality { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int Number { get; set; }

    public string? Avatar { get; set; }

    public string? LinkFb { get; set; }

    public string? LinkIg { get; set; }

    public string ClubId { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public bool? IsProcessed { get; set; }

    public virtual Club Club { get; set; } = null!;
}
