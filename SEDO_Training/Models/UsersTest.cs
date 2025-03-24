using System;
using System.Collections.Generic;

namespace SEDO_Training.Models;

public partial class UsersTest
{
    public int Id { get; set; }

    public int? Users { get; set; }

    public int? Tests { get; set; }

    public int? Points { get; set; }

    public virtual Test? TestsNavigation { get; set; }

    public virtual User? UsersNavigation { get; set; }
}
