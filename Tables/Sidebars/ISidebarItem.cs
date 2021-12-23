using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars
{
    public interface ISidebarItem
    {
        public string? Text { get; }

        public string? Link { get; }

        public string? SvgTag { get; }

        public IEnumerable<ISidebarItem> Children { get; }

        internal SidebarItemType ItemType { get; } 
    }

    internal enum SidebarItemType
    {
        NavHeader,
        NavItem,
    }
}
