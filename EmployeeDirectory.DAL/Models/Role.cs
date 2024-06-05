using System;
using System.Collections.Generic;

namespace EmployeeDirectory.DAL.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Location { get; set; }

    public int Department { get; set; }

    public string? Description { get; set; }

    public virtual Department DepartmentNavigation { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Location LocationNavigation { get; set; } = null!;
}
