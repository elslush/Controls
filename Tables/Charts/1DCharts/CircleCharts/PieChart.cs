using SkiaSharp;

namespace Controls.Charts._1DCharts.CircleCharts;

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

    private SKPoint center;
    private SKRect bounds;
    public SKRect Bounds 
    {
        get => bounds;
        set
        {
            bounds = value;
            center = new(bounds.MidX, bounds.MidY);
        }
    }

    private const float DegreeToRadian = MathF.PI * 2 / 360;

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info)
    {
        //if (shouldComputeOffset)
        //{
        //    shouldComputeOffset = false;
        //    explodeOffset = ComputeExplodeOffet(Data.Select(LabelSelector), TextPaint);
        //}

        float radius = Math.Min(bounds.Width / 2, bounds.Height / 2) - explodeOffset;
        SKRect rect = new(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius);

        float startAngle = 0;

        var labels = Data.Select(LabelSelector);

        foreach (var item in Data)
        {
            var sweepAngle = 360f * PercentSelector.Invoke(item);

            // arc
            using (SKPath path = new())
            {
                path.MoveTo(center);
                path.ArcTo(rect, startAngle, sweepAngle, false);
                path.Close();

                canvas.DrawPath(path, PaintSelector.Invoke(item));
                canvas.DrawPath(path, OutlinePaint);
            }
                
            // label
            var text = LabelSelector.Invoke(item);

            float angle = startAngle + (sweepAngle / 2);
            var x = rect.MidX + (rect.Width / 4) * MathF.Cos(DegreeToRadian * angle);
            var y = rect.MidY + (rect.Height / 4) * MathF.Sin(DegreeToRadian * angle);

            TextPaint.TextAlign = SKTextAlign.Center;

            canvas.DrawText(text, x, y, TextPaint);
            canvas.DrawLine(center, new SKPoint(x, y), TextPaint);

            startAngle += sweepAngle;
        }
    }

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds)
    {
        throw new NotImplementedException();
    }
}
