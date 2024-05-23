namespace T_Shop.Shared.DTOs.Brand.ResponseModel;
public class BrandResponseModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public List<ModelOfBrand>? Models { get; set; }
}

public class ModelOfBrand
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}

