using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class TypeNews
{
    public int TypeNewsId { get; set; }

    public string TypeNewsName { get; set; } = null!;

    public string? TypeNewsDes { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
