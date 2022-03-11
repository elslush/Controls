using SkiaSharp;

namespace Controls.Charts.Base;

public readonly record struct Title : IDrawable
{
    private readonly string text;
    private readonly SKPaint paint;

    public Title(string text, SKPaint paint)
    {
        this.text = text;
        this.paint = paint;
    }

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds) 
    {
        paint.TextSize = IDrawable.BoundText(paint, bounds, text);
        var point = IDrawable.CenterText(paint, bounds, text);
        canvas.DrawText(text, point, paint);
    }
}