using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars
{
    public class NavHeader : ISidebarItem
    {
        public NavHeader(string? text, IEnumerable<ISidebarItem> subHeaders)
        {
            Text = text;
            Children = subHeaders;

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
