using Controls.Selection;

namespace Controls.Sidebars
{
    public class NavHeader : ISidebarItem
    {
        public event EventHandler<bool>? OnSelect;
        private bool isSelected;

        public NavHeader(string? text, NavColors navItemColors, IEnumerable<ISidebarItem> subHeaders)
        {
            Text = text;
            NavItemColors = navItemColors;
            Children = subHeaders;

            if (!subHeaders.Any())
                throw new ArgumentException("Sub Headers Must not be Empty");
        }

        public string? Text { get; }

        public string? Link => string.Empty;

        public string? SvgTag => string.Empty;

        public IEnumerable<ISidebarItem> Children { get; }

        public SidebarItemType ItemType => SidebarItemType.NavHeader;

        public SelectionStyle SelectionStyle { get; set; }

        public bool IsSelected => isSelected;

        public NavColors NavItemColors { get; set; }

        public void Select(bool isSelected, bool dispatchEvent = true)
        {
            this.isSelected = isSelected;
            if (dispatchEvent)
                OnSelect?.Invoke(this, isSelected);
        }
    }
}
