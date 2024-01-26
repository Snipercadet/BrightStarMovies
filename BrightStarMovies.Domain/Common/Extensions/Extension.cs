using BrightStarMovies.Domain.Abstractions;

namespace BrightStarMovies.Domain.Common.Extensions
{
    public static class Extension
    {
        public static List<T> ToPageSize<T>(this List<T> records, int pageIndex, int pageSize) where T : class
        {
            return records.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public static List<T> FilterFromDate<T>(this List<T> records, DateTime fromDate) where T : BaseEntity
        {
            return records.Where((e) => e.CreatedAt >= fromDate).ToList();
        }

        public static List<T> FilterToDate<T>(this List<T> records, DateTime toDate) where T : BaseEntity
        {
            return records.Where((e) => e.CreatedAt <= toDate).ToList();
        }
    }
}
