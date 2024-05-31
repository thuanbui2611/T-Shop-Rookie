using T_Shop.Shared.DTOs.Transaction.ResponseModel;
using T_Shop.Shared.ViewModels.ProductsPage;

namespace T_Shop.Shared.ViewModels.TransactionPage;
public class TransactionVM
{
    public List<TransactionResponseModel> Transactions { get; set; } = [];
    public MetaData PaginationMetaData { get; set; }
}
