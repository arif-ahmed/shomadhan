namespace Somadhan.Domain.Interfaces;
public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    string? CreatedBy { get; set; }
    DateTime? ModifiedAt { get; set; }
    string? ModifiedBy { get; set; }
    string? DeletedBy { get; set; }
    string? DeletedAt { get; set; }
}