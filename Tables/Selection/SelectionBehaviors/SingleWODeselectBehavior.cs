using Controls.Disabling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection.SelectionBehaviors
{
    public class SingleWODeselectBehavior<T> : ISelectionBehavior<T> where T : ISelectable, IDisablable
    {
        public void Rebase(ISelectionBehavior<T>? previousSelectionBehavior, HashSet<T> selected, HashSet<T> nonSelected)
        {
            if (previousSelectionBehavior is MultipleBehavior<T>)
                DeselectAll(selected, nonSelected);
        }

        public void Select(T item, HashSet<T> selected, HashSet<T> nonSelected)
        {
            if (selected.Add(item))
            {
                DeselectAll(selected, nonSelected, item);
                item.Select(true);
                nonSelected.Remove(item);
            }
        }

        public void Deselect(T item, HashSet<T> selected, HashSet<T> nonSelected)
        {
        }

        private static void DeselectAll(HashSet<T> selected, HashSet<T> nonSelected, params T[] excempt)
        {
            var deselected = selected.Except(excempt);
            foreach (var other in deselected)
                other.Select(false);

            nonSelected.UnionWith(deselected);
            selected.IntersectWith(excempt);
        }
    }
}
