namespace EmployeeManagement.Dtos;

public class GenericResponse<T> : ApiResponse
{
    public T? data { get; set; }
}
