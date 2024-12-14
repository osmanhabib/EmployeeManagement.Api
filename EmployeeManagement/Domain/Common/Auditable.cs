namespace EmployeeManagement.Domain.Common;

public class Auditable : Base
{
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? ModifiedOn { get; set; } = null;
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
