using System;
using System.Collections.Generic;

namespace SEDO_Training.Models;

public partial class Test
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public int? Points { get; set; }

    public virtual ICollection<UsersTest> UsersTests { get; set; } = new List<UsersTest>();
}
