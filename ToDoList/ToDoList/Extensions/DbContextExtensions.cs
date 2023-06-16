using ToDoList.Models;

namespace ToDoList.Extensions
{
    public static class DbContextExtensions
    {
        public static void UpdateManyToMany<T, KEY>(this ToDoListDbContext db, IEnumerable<T> currentItems, IEnumerable<T> newItem, Func<T, KEY> getKey) where T : class
        {
            if (currentItems != null)
            {
                db.Set<T>().RemoveRange(currentItems.Except(newItem, getKey));
                db.Set<T>().AddRange(newItem.Except(currentItems, getKey));
            }
            else
            {
                db.Set<T>().AddRange(newItem);
            }
        }

        //Check
        public static IEnumerable<T> Except<T, KEY>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, KEY> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, tmp) => new { t, tmp })
                .Where(t => ReferenceEquals(null, t.tmp) || t.tmp.Equals(default(T)))
                .Select(t => t.t.item);
        }

    }
}
