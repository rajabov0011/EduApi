
namespace EduApi.Infrastructure.Extensions;

internal static class CollectionExtensions
{
    public static IQueryable<T> ToPaged<T>(this IQueryable<T> query, int pageNumber, int pageSize) =>
        query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}
