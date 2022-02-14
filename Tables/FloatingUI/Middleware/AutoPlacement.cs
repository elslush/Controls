namespace Controls.FloatingUI.Middleware;

public class AutoPlacement : IMiddleware
{
    public AutoPlacement() { }
    public AutoPlacement(IEnumerable<Placement> allowedPlacements)
    {
        Options = new
        {
            allowedPlacements,
        };
    }

    public string Name => "autoPlacement";

    public object? Options { get; }
}
