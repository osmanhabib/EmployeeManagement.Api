namespace EmployeeManagement.Features.Employees.Commands;

public class CreateEmployeeCommand
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string DesignationId { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}
