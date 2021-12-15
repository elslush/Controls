using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Dropdowns
{
    internal class DropdownManager
    {
        private readonly IClickManager clickManager;
        private readonly string clickOrigin;
        private bool collapse = false;

        internal DropdownManager(string clickOrigin, IClickManager clickManager)
        {
            this.clickOrigin = clickOrigin;
            this.clickManager = clickManager;
            this.clickManager.HasClicked += Close;
        }

        public event EventHandler<bool> CollapsedChange;

        public void Toggle()
        {
            if (!collapse)
                clickManager.Click(clickOrigin);
            collapse = !collapse;
            CollapsedChange?.Invoke(this, collapse);
        }

        private void Close(object? sender, string origin)
        {
            if (collapse && origin != clickOrigin)
            {
                collapse = false;
                CollapsedChange?.Invoke(this, collapse);
            }
        }
    }
}
