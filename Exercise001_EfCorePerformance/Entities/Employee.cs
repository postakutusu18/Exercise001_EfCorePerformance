﻿namespace Exercise001_EfCorePerformance.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int CompanyId { get; set; }
}
