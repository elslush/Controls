using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection
{
    public interface ISelectable
    {
        public event EventHandler<bool>? OnSelect;

        public void Select(bool isSelected, bool dispatchEvent = true);
        
        public bool IsSelected { get; }
    }
}
