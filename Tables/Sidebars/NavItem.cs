using Controls.Lists;
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
        public NavItem(string? text, string? link, string? svgTag)
        {
            Text = text;
            Link = link;
            SvgTag = svgTag;
        }

        public string? Text { get; }

        public string? Link { get; }

        public string? SvgTag { get; }

        public IEnumerable<ISidebarItem> Children => Enumerable.Empty<ISidebarItem>();

        SidebarItemType ISidebarItem.ItemType => SidebarItemType.NavItem;
    }
}
