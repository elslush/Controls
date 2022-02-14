namespace Controls.FloatingUI.Middleware;

public class Inline : IMiddleware
{
    public Inline() { }
    public Inline(int clientX, int clientY)
    {
        Options = new
        {
            clientX,
            clientY
        };
    }

    public string Name => "inline";

    public object? Options { get; }
}
