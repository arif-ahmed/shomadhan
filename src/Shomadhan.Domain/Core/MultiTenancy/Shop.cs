using Shomadhan.Domain;
using Shomadhan.Domain.Interfaces;

public class Shop : EntityBase, IAuditable
{
    public string Name { get; set; } = default!;
    public string? Address { get; set; } 
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpeningHours { get; set; }
    public string? ClosingHours { get; set; }
    public string? OwnerName { get; set; }
    public bool IsApproved { get; set; }
    public bool IsLocked { get; set; }

    #region Auditable Properties
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public string? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
    #endregion
}
