using Controls.Selection;

namespace Controls.Lists;

public class IListItem : SelectableValue
{
    public IListItem(in SelectionCollection selectionCollection) 
        : base(selectionCollection)
    {
    }

    public SelectionStyle SelectionStyle { get; set; }
}