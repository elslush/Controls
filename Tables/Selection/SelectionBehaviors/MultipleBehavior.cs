using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection.SelectionBehaviors
{
    public class MultipleBehavior : ISelectionBehavior
    {
        private bool isDisabled;

        public MultipleBehavior(int maxCount)
        {
            MaxCount = maxCount;
        }

        public int MaxCount { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MultipleBehavior other && other.MaxCount == MaxCount;
        }

        public override int GetHashCode() => MaxCount.GetHashCode();

        void ISelectionBehavior.Rebase(ISelectionBehavior? previousSelectionBehavior, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected)
        {
        }

        void ISelectionBehavior.Select(SelectableValue item, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected)
        {
            if (selected.Add(item))
            {
                item.Select(true);
                nonSelected.Remove(item);
            }

            if (selected.Count > MaxCount && !isDisabled)
            {
                foreach (var value in nonSelected)
                    value.Disable(true);
                isDisabled = true;
            }
            else if (isDisabled)
            {
                foreach (var value in nonSelected)
                    value.Disable(false);
                isDisabled = false;
            }
        }

        void ISelectionBehavior.Deselect(SelectableValue item, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected)
        {
            if (selected.Remove(item))
            {
                item.Select(false);
                nonSelected.Add(item);
            }
        }
    }
}
