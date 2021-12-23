using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Sidebars
{
    public record NavHeader : ISidebarItem
    {
        public NavHeader(string? text, IEnumerable<ISidebarItem> subHeaders)
        {
            Text = text;
            Children = subHeaders;

            if (!subHeaders.Any())
                throw new ArgumentException("Sub Headers Must not be Empty");
        }

        public string? Text { get; }

        public string? Link => string.Empty;

        public string? SvgTag => string.Empty;

        public IEnumerable<ISidebarItem> Children { get; }

        SidebarItemType ISidebarItem.ItemType => SidebarItemType.NavHeader;
    }
}
