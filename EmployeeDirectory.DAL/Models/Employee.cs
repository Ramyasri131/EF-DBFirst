using System;
using System.Collections.Generic;

namespace EmployeeDirectory.DAL.Models;

public partial class Employee
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long MobileNumber { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public DateOnly DateOfJoin { get; set; }

    public int Location { get; set; }

    public int JobTitle { get; set; }

    public int Department { get; set; }

    public int Manager { get; set; }

    public int Project { get; set; }

    public virtual Department DepartmentNavigation { get; set; } = null!;

    public virtual Role JobTitleNavigation { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;

    public virtual Manager ManagerNavigation { get; set; } = null!;

    public virtual Project ProjectNavigation { get; set; } = null!;
}
