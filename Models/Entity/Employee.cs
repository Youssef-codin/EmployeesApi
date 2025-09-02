using System;
using System.Collections.Generic;

namespace EmployeeApi.Models.Entity;

public partial class Employee
{
    public string Name { get; set; } = null!;

    public decimal? Salary { get; set; }

    public string Department { get; set; } = null!;

    public string? Manager { get; set; }
}
