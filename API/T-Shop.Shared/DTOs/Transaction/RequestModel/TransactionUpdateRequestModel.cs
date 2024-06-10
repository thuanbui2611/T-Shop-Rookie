namespace T_Shop.Shared.DTOs.Transaction.RequestModel;
public class TransactionUpdateRequestModel
{
    public Guid ID { get; set; }
    public required string Status { get; set; }
    public string Reason { get; set; } = string.Empty;
}
