namespace EmployeeManagement.Dtos;

public record LoginRequest(
    string Email,
    string Password);
