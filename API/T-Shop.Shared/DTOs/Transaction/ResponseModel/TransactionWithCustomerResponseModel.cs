using T_Shop.Shared.DTOs.Order.ResponseModel;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Shared.DTOs.Transaction.ResponseModel;
public class TransactionWithCustomerResponseModel
{
    public Guid ID { get; set; }
    public required string Status { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderResponseModel Order { get; set; }
    public UserResponseModel User { get; set; }
}
