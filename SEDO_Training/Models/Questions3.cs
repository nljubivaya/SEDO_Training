using System;
using System.Collections.Generic;

namespace SEDO_Training.Models;

public partial class Questions3
{
    public int Id { get; set; }

    public string? Questiontext { get; set; }

    public int Correctanswerindex { get; set; }

    public string? Answer1 { get; set; }

    public string? Answer2 { get; set; }

    public string? Answer3 { get; set; }

    public int? Selectedanswerindex { get; set; }
}
