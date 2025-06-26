namespace MC.IMS.API.Models.Result.Reference;

public class ProductCategory : ResultBaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}