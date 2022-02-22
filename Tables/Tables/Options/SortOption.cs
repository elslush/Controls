﻿using Controls.Data;

namespace Controls.Tables.Options;

public readonly struct SortOption<T> : IOption<T>
{
    private readonly Func<IQueryable<T>, SortBy<T>> sortBy;

    public SortOption(Func<IQueryable<T>, SortBy<T>> sortBy, bool isAscending, uint sortOrder = 0)
    {
        this.sortBy = sortBy;
        IsAscending = isAscending;
        SortOrder = sortOrder;
    }

    public bool IsAscending { get; }
    
    public uint SortOrder { get; }

    public SortBy<T> Evaluate(IQueryable<T> source) => sortBy(source);

    public IQueryable<T> Transform(IQueryable<T> source) => sortBy(source).Apply(source, IsAscending);
}
