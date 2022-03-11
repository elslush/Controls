using SkiaSharp;

namespace Controls.Charts.Base;

public interface IDrawable
{
    void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds);

    public static float BoundText(in SKPaint paint, in SKRect bounds, string text)
    {
        SKRect textBounds = new();
        paint.MeasureText(text, ref textBounds);
        var widthSize = 0.95f * bounds.Width * paint.TextSize / textBounds.Width;
        var heightSize = 0.95f * bounds.Height * paint.TextSize / textBounds.Height;
        return Math.Min(widthSize, heightSize);
    }

    public static SKPoint CenterText(in SKPaint paint, in SKRect bounds, string text)
    {
        SKRect textBounds = new();
        paint.MeasureText(text, ref textBounds);
        float xText = bounds.MidX - textBounds.MidX;
        float yText = bounds.MidY - textBounds.MidY;

        return new(xText, yText);
    }
}
