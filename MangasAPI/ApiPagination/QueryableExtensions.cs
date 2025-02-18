namespace MangasAPI.ApiPagination
{
    using MangasAPI.Entities;

    public static class QueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, Pagination pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.AmountPerPage)
                .Take(pagination.AmountPerPage);
        }
    }
}
