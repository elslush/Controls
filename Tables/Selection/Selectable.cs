using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection
{
    public class Selectable
    {
        public event EventHandler<bool>? OnSelect;
        public event EventHandler<bool>? OnDisable;

        public void Select(bool isSelected) => OnSelect?.Invoke(this, isSelected);
        public void Disable(bool isDisabled) => OnDisable?.Invoke(this, isDisabled);

        public SelectionColors SelectionColors { get; set; }
    }
}
