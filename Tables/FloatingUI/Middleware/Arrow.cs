using Microsoft.AspNetCore.Components;

namespace Controls.FloatingUI.Middleware;

public class Arrow : IMiddleware
{
    public Arrow(ElementReference arrowReference, int padding = 0)
    {
        Options = new
        {
            element = arrowReference,
            padding,
        };
    }

    public string Name => "arrow";

    public object? Options { get; }
}
