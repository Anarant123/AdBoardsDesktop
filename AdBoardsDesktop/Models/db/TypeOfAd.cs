using System;
using System.Collections.Generic;

namespace AdBoardsDesktop.Models.db;

public partial class TypeOfAd
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();
}
