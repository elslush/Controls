using Controls.Selection;
using Controls.Selection.SelectionBehaviors;

namespace Controls.Sidebars
{
    public class NavItem : ISidebarItem
    {
        public NavItem(string? text, string? link, string? svgTag, SelectionColors selectionColors)
            : this(text, link, svgTag, selectionColors, new SelectionCollection(ISelectionBehavior.Single))
        {}

        public NavItem(string? text, string? link, string? svgTag, SelectionColors selectionColors, in SelectionCollection selectionCollection)
            : base(selectionCollection)
        {
            Text = text;
            Link = link;
            SvgTag = svgTag;
            SelectionColors = selectionColors;
        }

        public override string? Text { get; }

        public override string? Link { get; }

        public override string? SvgTag { get; }

        public override IEnumerable<ISidebarItem> Children => Enumerable.Empty<ISidebarItem>();

        internal override SidebarItemType ItemType => SidebarItemType.NavItem;
    }
}
