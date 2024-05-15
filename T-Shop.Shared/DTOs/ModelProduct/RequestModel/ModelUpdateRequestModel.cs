namespace T_Shop.Shared.DTOs.ModelProduct.RequestModel;
public class ModelUpdateRequestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public Guid BrandID { get; set; }
}
