using Controls.Charts.Base;
using Controls.Charts.Markers;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Charts.Legends.LegendElements;

public readonly record struct IconElement : ILegendElement, IDisposable
{
    private const int markerWidth = 20;

    private readonly SKPaint titlePaint = new()
    {
        Color = SKColors.Black,
        IsAntialias = true,
        TextSize = 24,
    };

    private readonly IMarker marker;
    private readonly string text;

    public IconElement(IMarker marker, string text)
    {
        this.marker = marker;
        this.text = text;
    }

    public float GetTextSize(SKRect bounds)
    {
        var textBounds = new SKRect(bounds.Left + markerWidth + 5, bounds.Top, bounds.Right, bounds.Bottom);
        return IDrawable.BoundText(titlePaint, textBounds, text);
    }

    public void SetTextSize(float textSize) => titlePaint.TextSize = textSize;

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds)
    {
        var markerBounds = new SKRect(bounds.Left, bounds.Top, bounds.Left + markerWidth, bounds.Bottom);
        marker.Draw(ref canvas, ref info, markerBounds);

        var textBounds = new SKRect(bounds.Left + markerWidth + 5, bounds.Top, bounds.Right, bounds.Bottom);
        var point = IDrawable.CenterText(titlePaint, textBounds, text);
        canvas.DrawText(text, textBounds.Left, point.Y, titlePaint);
    }

    public void Dispose()
    {
        titlePaint.Dispose();
        GC.SuppressFinalize(this);
    }
}
