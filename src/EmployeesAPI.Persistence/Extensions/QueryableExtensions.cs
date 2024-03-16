namespace EmployeeAPI.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> items, int page, int offset) where T : class
        {
            items = items
                .Skip(Math.Abs(offset * (page - 1)))
                .Take(Math.Abs(offset + 1));

            return items;
        }

        public static IEnumerable<T> ApplyPagination<T>(this IEnumerable<T> items, int page, int offset) where T : class
        {
            items = items
                .Skip(Math.Abs(offset * (page - 1)))
                .Take(Math.Abs(offset + 1));

            return items;
        }
    }
}