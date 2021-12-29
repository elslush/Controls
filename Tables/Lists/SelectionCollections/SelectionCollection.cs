using Controls.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Lists.SelectionCollections
{
    public class SelectionCollection<T> where T : Selectable
    {
        private readonly HashSet<T> selected = new(), nonSelected = new();
        private SelectionType? previousSelectionType = null;
        private Action<T>? select;

        public SelectionType Type { get; private set; }

        public SelectionCollection(SelectionType selectionType) => SetSelectionType(selectionType);

        public IReadOnlyCollection<T> Selected => selected;

        public IReadOnlyCollection<T> NonSelected => nonSelected;

        public event EventHandler? SelectionChanged;

        public void SetSelectionType(SelectionType selectionType)
        {
            Type = selectionType;
            switch (selectionType)
            {
                case SelectionType.Single:
                    select = SelectSingle;
                    break;
                case SelectionType.Multiple:
                    select = SelectMultiple;
                    break;
            }

            if (previousSelectionType == SelectionType.Multiple && selectionType == SelectionType.Single)
                DeselectAll();

            previousSelectionType = selectionType;
        }

        public void Deselect(T item)
        {
            if (selected.Remove(item))
            {
                item.Select(false);
                nonSelected.Add(item);
            }
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Select(T item) => select?.Invoke(item);

        private void SelectSingle(T item)
        {
            if (selected.Add(item))
            {
                DeselectAll(item);
                item.Select(true);
                nonSelected.Remove(item);
            }
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SelectMultiple(T item)
        {
            if (selected.Add(item))
            {
                item.Select(true);
                nonSelected.Remove(item);
            }
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Add(IEnumerable<T> items) => nonSelected.UnionWith(items);

        public void Remove(IEnumerable<T> items)
        {
            nonSelected.ExceptWith(items);
            selected.ExceptWith(items);
        }

        public void Clear()
        {
            selected.Clear();
            nonSelected.Clear();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeselectAll()
        {
            DeselectAll(Array.Empty<T>());
            SelectionChanged?.Invoke(this, EventArgs.Empty);
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
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
