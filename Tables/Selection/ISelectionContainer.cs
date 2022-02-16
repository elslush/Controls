namespace Controls.Selection;

public interface ISelectionContainer<T>
{
    public Task AddValue(T value, bool dispatchOverride = false);

    public Task RemoveValue(T value);

    public bool IsSelected(T value);

    public Task Clear();
}