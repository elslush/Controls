using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection.SelectionBehaviors
{
    public interface ISelectionBehavior
    {
        private static readonly SingleBehavior singleBehavior = new();
        private static readonly SingleWODeselectBehavior singleWODeselectBehavior = new();

        public static SingleBehavior Single => singleBehavior;
        public static SingleWODeselectBehavior SingleWODeselect => singleWODeselectBehavior;

        internal void Rebase(ISelectionBehavior? previousSelectionBehavior, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected);
        internal void Select(SelectableValue item, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected);
        internal void Deselect(SelectableValue item, HashSet<SelectableValue> selected, HashSet<SelectableValue> nonSelected);
    }
}
