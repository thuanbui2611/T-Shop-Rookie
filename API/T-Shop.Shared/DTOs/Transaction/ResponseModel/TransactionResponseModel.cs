using T_Shop.Shared.DTOs.Order.ResponseModel;

namespace T_Shop.Shared.DTOs.Transaction.ResponseModel;
public class TransactionResponseModel
{
    public Guid ID { get; set; }
    public required string Status { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderResponseModel Order { get; set; }
}

