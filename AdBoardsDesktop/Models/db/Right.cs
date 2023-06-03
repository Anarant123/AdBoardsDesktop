using System;
using System.Collections.Generic;

namespace AdBoardsDesktop.Models.db;

public partial class Right
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
