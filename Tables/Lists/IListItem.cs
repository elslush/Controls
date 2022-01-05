using Controls.Selection;

namespace Controls.Lists;

public interface IListItem
{
    public SelectionStyle SelectionStyle { get; set; }
}