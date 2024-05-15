namespace T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
public class ModelProductResponseModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public BrandOfModel Brand { get; set; }

    public class BrandOfModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }

}
