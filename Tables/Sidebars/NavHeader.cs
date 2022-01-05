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
        public event EventHandler<bool>? OnSelect;
        private bool isSelected;

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

        public SidebarItemType ItemType => SidebarItemType.NavHeader;

        public SelectionStyle SelectionStyle { get; set; }

        public bool IsSelected => isSelected;

        public void Select(bool isSelected)
        {
            this.isSelected = isSelected;
            OnSelect?.Invoke(this, isSelected);
        }
    }
}
