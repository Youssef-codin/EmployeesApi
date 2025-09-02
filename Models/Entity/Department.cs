using System;
using System.Collections.Generic;

namespace EmployeeApi.Models.Entity;

public partial class Department
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
