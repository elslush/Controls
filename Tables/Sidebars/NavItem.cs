using Controls.Disabling;
using Controls.Selection;

namespace Controls.Sidebars
{
    public class NavItem : ISidebarItem, IDisablable
    {
        public event EventHandler<bool>? OnSelect;
        private bool isSelected;

        public NavItem(string? text, string? link, string? svgTag, NavColors navItemColors)
        {
            Text = text;
            Link = link;
            SvgTag = svgTag;
            NavItemColors = navItemColors;
        }

        public string? Text { get; }

        public string? Link { get; }

        public string? SvgTag { get; }

        public IEnumerable<ISidebarItem> Children => Enumerable.Empty<ISidebarItem>();

        public SelectionStyle SelectionStyle { get; set; }

        public SidebarItemType ItemType => SidebarItemType.NavItem;

        public bool IsSelected => isSelected;

        public NavColors NavItemColors { get; set; }

        public void Select(bool isSelected, bool dispatchEvent = true)
        {
            this.isSelected = isSelected;
            if (dispatchEvent)
                OnSelect?.Invoke(this, isSelected);
        }

        public void Disable(bool isDisabled) => throw new InvalidOperationException();
    }
}
