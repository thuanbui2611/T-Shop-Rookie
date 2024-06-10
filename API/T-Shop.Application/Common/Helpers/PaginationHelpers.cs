using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.Application.Common.Helpers;
public static class PaginationHelpers
{
    public static (IEnumerable<T>, PaginationMetaData) GetPaginationModel<T>(IEnumerable<T> items, PaginationRequestModel pagination) where T : class
    {
        var totalItemCount = items.Count();

        var paginationMetaData = new PaginationMetaData(totalItemCount, pagination.pageSize, pagination.pageNumber);

        var itemsToReturn = items
            .Skip(pagination.pageSize * (pagination.pageNumber - 1))
            .Take(pagination.pageSize);

        return (itemsToReturn, paginationMetaData);
    }
}
