using Controls.Selection;
using Controls.Selection.SelectionBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars
{
    public class NavHeader : ISidebarItem
    {
        public NavHeader(string? text, IEnumerable<ISidebarItem> subHeaders, SelectionColors selectionColors)
            : this(text, subHeaders, selectionColors, new SelectionCollection(ISelectionBehavior.Single))
        {}

        public NavHeader(string? text, IEnumerable<ISidebarItem> subHeaders, SelectionColors selectionColors, in SelectionCollection selectionCollection)
            : base(selectionCollection)
        {
            Text = text;
            Children = subHeaders;
            SelectionColors = selectionColors;

            if (!subHeaders.Any())
                throw new ArgumentException("Sub Headers Must not be Empty");
        }

        public override string? Text { get; }

        public override string? Link => string.Empty;

        public override string? SvgTag => string.Empty;

        public override IEnumerable<ISidebarItem> Children { get; }

        internal override SidebarItemType ItemType => SidebarItemType.NavHeader;
    }
}
