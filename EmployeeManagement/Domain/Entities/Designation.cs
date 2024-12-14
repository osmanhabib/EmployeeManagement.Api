using EmployeeManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities;
public class Designation : Auditable
{
    [Key]
    public int DesignationId { get; set; }
    public string DesignationName { get; set; } = string.Empty;
}
