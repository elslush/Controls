using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection.SelectionBehaviors
{
    public interface ISelectionBehavior<T>
    {
        public void Rebase(ISelectionBehavior<T>? previousSelectionBehavior, HashSet<T> selected, HashSet<T> nonSelected);
        public void Select(T item, HashSet<T> selected, HashSet<T> nonSelected);
        public void Deselect(T item, HashSet<T> selected, HashSet<T> nonSelected);
    }
}
