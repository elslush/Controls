namespace Controls.FloatingUI.Middleware;

public class Offset : IMiddleware
{
    public Offset(int distance) => Options = distance;

    public Offset(int mainAxis, int crossAxis) => Options = new
    {
        mainAxis,
        crossAxis
    };

    public string Name => "offset";

    public object? Options { get; }
}
