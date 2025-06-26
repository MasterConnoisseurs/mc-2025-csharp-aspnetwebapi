namespace MC.IMS.API.Models.Result.Reference;

public class Clients : ResultBaseEntity
{
    public string InsuranceCustomerNo { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Suffix { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public bool IsMale { get; set; }
    public long ValidIdPresented { get; set; }
    public string ValidIdPresentedNumber { get; set; } = string.Empty;
    public DateTime ValidIdExpiration { get; set; }
    public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }
    public long AddressTownId { get; set; }
    public string Address { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}