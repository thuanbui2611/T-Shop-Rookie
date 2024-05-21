namespace T_Shop.Shared.DTOs.Pagination;
public class PaginationMetaData
{
    public int TotalItemCount { get; set; }
    public int TotalPageCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }

    public PaginationMetaData(int totalItemCount, int pageSize, int currentPage)
    {
        TotalItemCount = totalItemCount;
        TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        PageSize = pageSize;
        CurrentPage = currentPage;
    }
}
