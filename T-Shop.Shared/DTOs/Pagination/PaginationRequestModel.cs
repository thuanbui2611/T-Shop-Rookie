namespace T_Shop.Shared.DTOs.Pagination;
public class PaginationRequestModel
{
    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 10;
}