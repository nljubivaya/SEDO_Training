﻿using System;
using System.Collections.Generic;

namespace SEDO_Training.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public int? Uk { get; set; }
}
