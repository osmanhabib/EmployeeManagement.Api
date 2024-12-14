using EmployeeManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities;
public class Department : Auditable
{
    [Key]
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}
