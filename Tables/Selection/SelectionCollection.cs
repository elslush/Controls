using Controls.Selection;
using Controls.Selection.SelectionBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection
{
    public class SelectionCollection
    {
        private readonly HashSet<SelectableValue> selected = new(), nonSelected = new();
        private bool isDisabled;
        private ISelectionBehavior previousSelectionBehavior;

        public SelectionCollection() : this(ISelectionBehavior.Single) { }

        public SelectionCollection(ISelectionBehavior selectionBehavior) => SetSelectionBehavior(selectionBehavior);

        public IReadOnlyCollection<SelectableValue> Selected => selected;

        public IReadOnlyCollection<SelectableValue> NonSelected => nonSelected;

        public void SetSelectionBehavior(ISelectionBehavior selectionBehavior)
        {
            selectionBehavior.Rebase(previousSelectionBehavior, selected, nonSelected);
            previousSelectionBehavior = selectionBehavior;
        }

        public void Deselect(SelectableValue item) =>  previousSelectionBehavior.Deselect(item, selected, nonSelected);

        public void Select(SelectableValue item) => previousSelectionBehavior.Select(item, selected, nonSelected);

        public void Add(IEnumerable<SelectableValue> items) => nonSelected.UnionWith(items);

        public void Add(SelectableValue item) => nonSelected.Add(item);

        public void Remove(SelectableValue item)
        {
            nonSelected.Remove(item);
            selected.Remove(item);
        }

        public void Remove(IEnumerable<SelectableValue> items)
        {
            nonSelected.ExceptWith(items);
            selected.ExceptWith(items);
        }

        public void Clear()
        {
            selected.Clear();
            nonSelected.Clear();
        }

        public void DeselectAll(params SelectableValue[] excempt)
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
