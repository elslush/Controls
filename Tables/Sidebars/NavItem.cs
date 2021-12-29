using Controls.Lists;
using Controls.Selection;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars
{
    public class NavItem : ISidebarItem
    {
        public NavItem(string? text, string? link, string? svgTag, SelectionColors selectionColors)
        {
            Text = text;
            Link = link;
            SvgTag = svgTag;
            SelectionColors = selectionColors;
            IsSelectable = true;
        }

        public override string? Text { get; }

        public override string? Link { get; }

        public override string? SvgTag { get; }

        public override IEnumerable<ISidebarItem> Children => Enumerable.Empty<ISidebarItem>();

        internal override SidebarItemType ItemType => SidebarItemType.NavItem;
    }
}
