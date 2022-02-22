namespace Controls.Selection;

public interface ISelectionContainer<T>
{
    public void AddValue(T value);

    public void RemoveValue(T value);

    public bool IsSelected(T value);

    public Task Notify();

    public void Clear();
}