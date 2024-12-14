using EmployeeManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities;
public class User : Auditable
{
    [Key]
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
