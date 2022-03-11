using SkiaSharp;

namespace Controls.Charts.Markers;

public readonly record struct CircleMarker : IMarker
{
    private readonly SKPaint paint;

    public CircleMarker(SKPaint paint)
    {
        this.paint = paint;
    }

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds)
    {
        var radius = Math.Min(bounds.Width, bounds.Height) / 2;
        float x = bounds.MidX;
        float y = bounds.MidY;

        canvas.DrawCircle(x, y, radius, paint);
    }
}
