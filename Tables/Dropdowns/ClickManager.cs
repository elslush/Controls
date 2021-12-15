using Microsoft.JSInterop;

namespace Controls.Dropdowns;

public interface IClickManager
{
    void Click(string origin);

    event EventHandler<string>? HasClicked;

    ValueTask SetupWindowClickCallback(IJSObjectReference module);
}

public class ClickManager : IClickManager, IDisposable
{
    private const string windowClickOrigin = "window";

    private readonly DotNetObjectReference<ClickManager> Reference;

    public ClickManager()
    {
        Reference = DotNetObjectReference.Create(this);
    }

    public void Click(string origin)
    {
        HasClicked?.Invoke(this, origin);
    }

    public event EventHandler<string>? HasClicked;

    [JSInvokable("ClickManager")]
    public void ClickWindow() => Click(windowClickOrigin);

    public ValueTask SetupWindowClickCallback(IJSObjectReference module) => module.InvokeVoidAsync("addWindowClickEventListener", Reference);

    public void Dispose() => Reference?.Dispose();
}