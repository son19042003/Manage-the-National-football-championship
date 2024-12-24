using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNum { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Avatar { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public string? RandomKey { get; set; }

    public DateTime? ResetKeyExpires { get; set; }

    public virtual Role Role { get; set; } = null!;
}
