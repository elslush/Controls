using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Controls.Tables.Columns;

public abstract class ColumnBase<TGridItem> : ComponentBase
{
    internal delegate void SetColumnOptions(ColumnOptions<TGridItem>? columnOptions);

    private static readonly RenderFragment EmptyColumnHeader = builder => { };

    //[CascadingParameter] internal Grid<TGridItem>.AddColumnCallback AddColumn { get; set; } = default!;

    [Parameter] public string? Title { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public Align Align { get; set; }
    [Parameter] public RenderFragment<TGridItem> CellContent { get; set; }

    protected string? ColumnClass
    {
        get
        {
            return Align switch
            {
                Align.Center => $"grid-col-center {Class}",
                Align.Right => $"grid-col-right {Class}",
                _ => Class,
            };
        }
    }

    public RenderFragment HeaderContent { get; set; } = EmptyColumnHeader;

    public ColumnBase()
    {
        HeaderContent = __builder => __builder.AddContent(0, Title);
    }

    internal virtual bool CanSort => false;

    internal virtual IQueryable<TGridItem> GetSortedItems(IQueryable<TGridItem> source, bool ascending) => source;

    //protected override void BuildRenderTree(RenderTreeBuilder builder)
    //{
    //    AddColumn(this);
    //}
}

public enum Align
{
    Left,
    Center,
    Right,
}
