using System.Linq.Expressions;

namespace Controls.Data;

public static class SortExtensions
{
    public static SortBy<T> SortByAscending<T, U>(this IQueryable<T> _, Expression<Func<T, U>> keySelector)
        => SortBy<T>.Create(keySelector);
}

public struct SortBy<T>
{
    private int _count;
    private readonly Func<IQueryable<T>, bool, IOrderedQueryable<T>> first;
    private readonly Func<IOrderedQueryable<T>, bool, IOrderedQueryable<T>> firstAppend;
    private readonly Func<IOrderedQueryable<T>, bool, IOrderedQueryable<T>>[] _then = new Func<IOrderedQueryable<T>, bool, IOrderedQueryable<T>>[10];

    internal SortBy(Func<IQueryable<T>, bool, IOrderedQueryable<T>> first, Func<IOrderedQueryable<T>, bool, IOrderedQueryable<T>> firstAppend)
    {
        this.first = first;
        this.firstAppend = firstAppend;
        _count = 0;
    }

    public static SortBy<T> Create<U>(Expression<Func<T, U>> expression)
        => new((queryable, asc) => asc ? queryable.OrderBy(expression) : queryable.OrderByDescending(expression),
            (queryable, asc) => asc ? queryable.ThenBy(expression) : queryable.OrderByDescending(expression));

    public SortBy<T> Then<U>(Expression<Func<T, U>> expression)
    {
        _then[_count++] = (queryable, asc) => asc ? queryable.ThenBy(expression) : queryable.ThenByDescending(expression);
        return this;
    }

    //public SortBy<T> Combine(SortBy<T> other)
    //{
    //    if (_count > 9)
    //        return this;

    //    _then[_count++] = other._first;
    //    foreach (var func in other._then)
    //    {
    //        if (_count > 9)
    //            return this;
    //        _then[_count++] = func;
    //    }
    //    return this;
    //}

    internal IOrderedQueryable<T> Apply(IQueryable<T> queryable, bool ascending)
    {
        var orderedQueryable = first(queryable, ascending);
        for (var i = 0; i < _count; i++)
            orderedQueryable = _then[i](orderedQueryable, ascending);
        return orderedQueryable;
    }

    internal IOrderedQueryable<T> Apply(IOrderedQueryable<T> queryable, bool ascending)
    {
        var orderedQueryable = firstAppend(queryable, ascending);
        for (var i = 0; i < _count; i++)
            orderedQueryable = _then[i](orderedQueryable, ascending);
        return orderedQueryable;
    }
}