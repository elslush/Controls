namespace Controls.FloatingUI.Middleware;

public class Flip : IMiddleware
{
    public string Name => "flip";

    public object? Options { get; }
}
