using Controls.Filtering;
using Controls.Paging;
using Controls.Sorting;

namespace Controls.ServerSide;

public class QueryProperties
{
    public string GlobalFilter { get; set; } = "";

    public HashSet<Filter> Filters { get; } = new();

    public HashSet<Sort> Sorts { get; } = new();

    public Pagination Pagination { get; set; } = new() { Skip = 0, Take = int.MaxValue, };
}
