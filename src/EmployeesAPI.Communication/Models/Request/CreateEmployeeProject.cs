﻿namespace EmployeesAPI.Common.Models.Request;

public class CreateEmployeeProject
{
    public int EmpID { get; set; }
    public int ProjectID { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
