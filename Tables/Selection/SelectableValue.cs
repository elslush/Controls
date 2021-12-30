using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection
{
    public class SelectableValue : IDisposable
    {
        private readonly SelectionCollection selectionCollection;

        public SelectableValue(in SelectionCollection selectionCollection)
        {
            this.selectionCollection = selectionCollection;
            this.selectionCollection.Add(this);
        }

        public event EventHandler<bool>? OnSelect;
        public event EventHandler<bool>? OnDisable;

        public void SelectFromCollection(bool isSelected)
        {
            if (isSelected)
                selectionCollection.Select(this);
            else
                selectionCollection.Deselect(this);
        }

        public void Select(bool isSelected) => OnSelect?.Invoke(this, isSelected);

        public void Disable(bool isDisabled) => OnDisable?.Invoke(this, isDisabled);

        public void Dispose() => selectionCollection.Remove(this);

        public SelectionColors SelectionColors { get; set; }
    }
}
