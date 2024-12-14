using EmployeeManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities;
public class Employee : Auditable
{
    [Key]
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DesignationId { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
}
