using Controls.Selection;

namespace Controls.Lists;

public class IListItem : Selectable
{
    public SelectionStyle SelectionStyle { get; set; }

    public bool IsSelectable { get; set; }
}