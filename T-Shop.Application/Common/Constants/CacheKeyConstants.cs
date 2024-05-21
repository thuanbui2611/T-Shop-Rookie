namespace T_Shop.Application.Common.Constants;
public class CacheKeyConstants
{
    public int ExpirationHours = 1;
    public string TypeCacheKey = "Type";
    public string BrandCacheKey = "Brand";
    public string ColorCacheKey = "Color";
    public string ModelCacheKey = "Model";
    public string ProductCacheKey = "Product";
    public string UserCacheKey = "User";

    public List<string> CacheKeyList = new List<string>();

    public void AddKeyToList(string key)
    {
        var isKeyExisted = CacheKeyList.Any(x => x.Equals(key));

        if (!isKeyExisted)
        {
            CacheKeyList.Add(key);
        }
    }
}
