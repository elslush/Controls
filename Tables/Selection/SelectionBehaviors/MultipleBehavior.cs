using Controls.Disabling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection.SelectionBehaviors
{
    public class MultipleBehavior<T> : ISelectionBehavior<T> where T : IDisablable, ISelectable
    {
        private bool isDisabled;

        public MultipleBehavior(int maxCount)
        {
            MaxCount = maxCount;
        }

        public int MaxCount { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MultipleBehavior<T> other && other.MaxCount == MaxCount;
        }

        public override int GetHashCode() => MaxCount.GetHashCode();

        public void Rebase(ISelectionBehavior<T>? previousSelectionBehavior, HashSet<T> selected, HashSet<T> nonSelected)
        {
        }

        public void Select(T item, HashSet<T> selected, HashSet<T> nonSelected)
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

        public void Deselect(T item, HashSet<T> selected, HashSet<T> nonSelected)
        {
            if (selected.Remove(item))
            {
                item.Select(false);
                nonSelected.Add(item);
            }
        }
    }
}
