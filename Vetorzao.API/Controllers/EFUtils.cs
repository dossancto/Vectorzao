namespace Vetorzao.API.Controllers;

public class DistanceWrap<T>
{
    public required T Entity { get; set; }
    public required float Distance { get; set; }
}

public static class EFUtils
{
    public static IQueryable<DistanceWrap<T>> SelectVector<T>(this IQueryable<T> query, float distance)
      => query.Select(x => new DistanceWrap<T>()
      {
          Entity = x,
          Distance = distance
      });

    public static IQueryable<DistanceWrap<T>> SelectVector<T>(this IQueryable<T> query, Func<T, double> distance)
      => query.Select(x => new DistanceWrap<T>()
      {
          Entity = x,
          Distance = (float)distance(x)
      });
}

