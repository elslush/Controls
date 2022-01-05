using Controls.Disabling;
using Controls.Selection;
using Controls.Selection.SelectionBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection
{
    public class SelectionCollection<T> where T : ISelectable, IDisablable
    {
        private readonly HashSet<T> selected = new(), nonSelected = new();
        private ISelectionBehavior<T> previousSelectionBehavior;

        public SelectionCollection(ISelectionBehavior<T> selectionBehavior) => SetSelectionBehavior(selectionBehavior);

        public IReadOnlyCollection<T> Selected => selected;

        public IReadOnlyCollection<T> NonSelected => nonSelected;

        public void SetSelectionBehavior(ISelectionBehavior<T> selectionBehavior)
        {
            selectionBehavior.Rebase(previousSelectionBehavior, selected, nonSelected);
            previousSelectionBehavior = selectionBehavior;
        }

        public void Select(T item, bool isSelected)
        {
            if (isSelected)
                 previousSelectionBehavior.Select(item, selected, nonSelected);
            else
                previousSelectionBehavior.Deselect(item, selected, nonSelected);
        }

        public void Add(IEnumerable<T> items) => nonSelected.UnionWith(items);

        public void Add(T item) => nonSelected.Add(item);

        public void Remove(T item)
        {
            nonSelected.Remove(item);
            selected.Remove(item);
        }

        public void Remove(IEnumerable<T> items)
        {
            nonSelected.ExceptWith(items);
            selected.ExceptWith(items);
        }

        public void Clear()
        {
            selected.Clear();
            nonSelected.Clear();
        }

        public void DeselectAll(params T[] excempt)
        {
            var deselected = selected.Except(excempt);
            foreach (var other in deselected)
                other.Select(false);

            nonSelected.UnionWith(deselected);
            selected.IntersectWith(excempt);
        }

        public void SelectAll()
        {
            foreach (var other in nonSelected)
                other.Select(true);

            selected.UnionWith(nonSelected);
            nonSelected.Clear();
        }
    }
}
