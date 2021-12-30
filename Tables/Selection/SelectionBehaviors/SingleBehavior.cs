using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection.SelectionBehaviors
{
    public class SingleBehavior : ISelectionBehavior
    {
        void ISelectionBehavior.Rebase(ISelectionBehavior? previousSelectionBehavior, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected)
        {
            if (previousSelectionBehavior is MultipleBehavior)
                DeselectAll(selected, nonSelected);
        }

        void ISelectionBehavior.Select(SelectableValue item, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected)
        {
            if (selected.Add(item))
            {
                DeselectAll(selected, nonSelected, item);
                item.Select(true);
                nonSelected.Remove(item);
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

        private static void DeselectAll(HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected, params SelectableValue[] excempt)
        {
            var deselected = selected.Except(excempt);
            foreach (var other in deselected)
                other.Select(false);

            nonSelected.UnionWith(deselected);
            selected.IntersectWith(excempt);
        }
    }
}
