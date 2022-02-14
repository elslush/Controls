namespace Controls.FloatingUI.Middleware;

public class Shift : IMiddleware
{
    public string Name => "shift";

    public object? Options { get; }
}
