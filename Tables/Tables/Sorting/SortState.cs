namespace Controls.Sorting;
public class SortState
{
    public event EventHandler<IEnumerable<Sort>>? OnSortAdd;
    public event EventHandler<IEnumerable<Sort>>? OnSortRemove;
    public event EventHandler? OnSortUpdate;

    public void RemoveSort(params Sort[] sortValues) => OnSortRemove?.Invoke(this, sortValues);

    public void AddSort(params Sort[] sortValues) => OnSortAdd?.Invoke(this, sortValues);

    public void UpdateSort() => OnSortUpdate?.Invoke(this, EventArgs.Empty);
}
