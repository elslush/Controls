namespace Controls.Data;

public static class PageExtensions
{
    public static PageBy<T> PageBy<T>(this IQueryable<T> source, int pageSize = 5, int pagerCount = 5)
        => new(source, pageSize, pagerCount);
}

public class PageBy<T>
{
    private readonly IQueryable<T> source;
    private int totalCount, totalPages, pagerCount, pageIndex, take, skip;

    internal PageBy(IQueryable<T> source, int pageSize, int pagerCount)
    {
        this.source = source;
        this.pagerCount = pagerCount;
        take = pageSize;
        skip = 0;

        RefreshSource();
    }

    public void SetPageSize(int pageSize)
    {
        take = pageSize;
        Page(pageIndex);
    }

    public void SetPagerCount(int pagerCount)
    {
        this.pagerCount = pagerCount;
        Page(pageIndex);
    }

    public void RefreshSource()
    {
        totalCount = source.Count();
        totalPages = (int)Math.Ceiling(totalCount / (double)take);
        PagerCount = totalPages - PagerStart;
    }

    public void Page(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < totalPages)
        {
            this.pageIndex = pageIndex;
            skip = pageIndex * take;
            PagerStart = pageIndex - (pageIndex % pagerCount);
            IsFirstPager = PagerStart == 0;
            PagerCount = totalPages - PagerStart;
            IsLastPager = PagerCount <= pagerCount;
            PagerEnd = PagerStart + (IsLastPager ? PagerCount : pagerCount);
            IsLastPage = pageIndex == totalPages - 1;
        }
    }

    public void IncrementPage() => Page(pageIndex + 1);

    public void DecrementPage() => Page(pageIndex - 1);

    public void IncrementPager() => Page(pageIndex - (pageIndex % pagerCount) + pagerCount);

    public void DecrementPager() => Page(pageIndex - (pageIndex % pagerCount) - pagerCount);

    public int PagerStart { get; private set; }
    public int PagerCount { get; private set; }
    public bool IsFirstPager { get; private set; }
    public bool IsLastPager { get; private set; }
    public int PagerEnd { get; private set; }
    public bool IsLastPage { get; private set; }

    internal IQueryable<T> Apply(IQueryable<T> queryable) => queryable.Skip(skip).Take(take);
}
