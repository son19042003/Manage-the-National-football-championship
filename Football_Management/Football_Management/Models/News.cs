using System;
using System.Collections.Generic;

namespace Football_Management.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string Title { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? ImgContent { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateU { get; set; }

    public int TypeNewsId { get; set; }

    public virtual TypeNews TypeNews { get; set; } = null!;
}
