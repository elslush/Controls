using Controls.Lists;
using Controls.Selection;

namespace Controls.Sidebars;

public interface ISidebarItem : IListItem, ISelectable
{
    internal SidebarItemType ItemType { get; }
}

public enum SidebarItemType
{
    NavHeader,
    NavItem,
}
