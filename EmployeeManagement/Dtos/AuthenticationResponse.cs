namespace EmployeeManagement.Dtos;

public record AuthenticationResponse(bool status,string message,string Token);
