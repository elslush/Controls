using SkiaSharp;

namespace Controls.Charts._1DCharts;

public class PieChart<T> : I1DChart where T : class
{
    private bool shouldComputeOffset = true;
    private IEnumerable<T> cachedData;
    private SKPaint cachedTextPaint;
    private float explodeOffset;

    public PieChart()
    {
        //OutlinePaint = new()
        //{
        //    Style = SKPaintStyle.Stroke,
        //    StrokeWidth = 5,
        //    IsAntialias = true,
        //    Color = SKColors.Black,
        //};
        //TextPaint = new()
        //{
        //    TextSize = 20,
        //    StrokeWidth = 5,
        //    IsAntialias = true,
        //    Color = SKColors.Black,
        //};
        //TextBackgroundPaint = new()
        //{
        //    Style = SKPaintStyle.Fill,
        //    Color = SKColors.Black,
        //};

        //var text = value.Percentage.ToString("P1");
    }

    public SKPaint OutlinePaint { get; set; }

    public SKPaint TextPaint 
    {
        get => cachedTextPaint;
        set
        {
            if (value != cachedTextPaint)
                shouldComputeOffset = true;
            cachedTextPaint = value;
        }
    }

    public SKPaint TextBackgroundPaint { get; set; }

    public IEnumerable<T> Data
    {
        get => cachedData;
        set
        {
            if (value != cachedData)
                shouldComputeOffset = true;
            cachedData = value;
        }
    }

    public Func<T, string> LabelSelector { get; set; }

    public Func<T, float> PercentSelector { get; set; }

    public Func<T, SKPaint> PaintSelector { get; set; }

    private static float ComputeExplodeOffet(IEnumerable<string> data, SKPaint textPaint)
    {
        float maxExplodeOffset = float.MinValue;
        SKRect frameRect = new();
        foreach (var item in data)
        {
            textPaint.MeasureText(item, ref frameRect);
            maxExplodeOffset = Math.Max(Math.Max(maxExplodeOffset, frameRect.Width), frameRect.Height);
        }
        return maxExplodeOffset;
    }

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds)
    {
        if (shouldComputeOffset)
        {
            shouldComputeOffset = false;
            explodeOffset = ComputeExplodeOffet(Data.Select(LabelSelector), TextPaint);
        }

        SKPoint center = new(bounds.MidX, bounds.MidY);
        float radius = Math.Min(bounds.Width / 2, bounds.Height / 2) - explodeOffset;
        SKRect rect = new(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius);

        float startAngle = 0;

        var labels = Data.Select(LabelSelector);

        foreach (var item in Data)
        {
            var sweepAngle = 360f * PercentSelector.Invoke(item);

            using SKPath path = new();
            path.MoveTo(center);
            path.ArcTo(rect, startAngle, sweepAngle, false);
            path.Close();

            var text = LabelSelector.Invoke(item);
            SKRect frameRect = new();
            TextPaint.MeasureText(text, ref frameRect);

            float angle = startAngle + 0.5f * sweepAngle;
            float x = radius * MathF.Cos(MathF.PI * angle / 180);
            float y = radius * MathF.Sin(MathF.PI * angle / 180);

            x += x > 0 ? frameRect.Width / 2 : -frameRect.Width / 2;
            y += y > 0 ? frameRect.Height / 2 : -frameRect.Height / 2;

            TextPaint.TextAlign = x > 0 ? SKTextAlign.Left : SKTextAlign.Right;

            canvas.DrawPath(path, PaintSelector.Invoke(item));
            canvas.DrawPath(path, OutlinePaint);

            var textX = rect.MidX + x;
            var textY = rect.MidY + y;
            canvas.DrawText(text, textX, textY, TextPaint);

            startAngle += sweepAngle;
        }
    }
}
