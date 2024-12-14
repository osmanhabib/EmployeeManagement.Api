namespace EmployeeManagement.Features.Employees.Commands;

public class UpdateEmployeeCommand
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string DesignationId { get; set; } = string.Empty;
}
