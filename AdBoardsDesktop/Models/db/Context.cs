using System;
using System.Collections.Generic;

namespace AdBoardsDesktop.Models.db;

public partial class Context 
{
    public static Person? UserNow { get; set; }
    public static Ad? AdNow { get; set; }
    public static AdListViewModel? AdList { get; set; }
}
