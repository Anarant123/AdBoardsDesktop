using System;
using System.Collections.Generic;
using AdBoards.ApiClient.Contracts.Responses;

namespace AdBoardsDesktop.Models.db;

public partial class Favorite
{
    public int Id { get; set; }

    public int? AdId { get; set; }

    public int? PersonId { get; set; }

    public virtual Ad? Ad { get; set; }

    public virtual Person? Person { get; set; }
}
