using Controls.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars
{
    public abstract class ISidebarItem : IListItem
    {
        public abstract string? Text { get; }

        public abstract string? Link { get; }

        public abstract string? SvgTag { get; }

        public abstract IEnumerable<ISidebarItem> Children { get; }

        internal abstract SidebarItemType ItemType { get; } 
    }

    internal enum SidebarItemType
    {
        NavHeader,
        NavItem,
    }
}
