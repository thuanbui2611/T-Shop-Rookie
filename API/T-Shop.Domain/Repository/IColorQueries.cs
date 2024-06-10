using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface IColorQueries
{
    Task<List<Color>> GetColorsAsync();
    Task<Color> GetColorByIdAsync(Guid id);
    Task<bool> CheckIsColorExisted(string name);
}
