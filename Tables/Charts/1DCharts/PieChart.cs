using SkiaSharp;

namespace Controls.Charts._1DCharts;

public readonly record struct PieChart : I1DChart, IDisposable
{
    private readonly SKPaint outlinePaint, textPaint, textBackgroundPaint;
    private readonly List<PieChartData> pieChartData;
    private readonly float explodeOffset;

    public PieChart(IEnumerable<I1DData> data)
    {
        outlinePaint = new()
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 5,
            IsAntialias = true,
            Color = SKColors.Black,
        };
        textPaint = new()
        {
            TextSize = 20,
            StrokeWidth = 5,
            IsAntialias = true,
            Color = SKColors.Black,
        };
        textBackgroundPaint = new()
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black,
        };

        pieChartData = new();

        float maxExplodeOffset = float.MinValue;
        foreach (var value in data)
        {
            var text = value.Percentage.ToString("P1");

            SKRect frameRect = new();
            textPaint.MeasureText(text, ref frameRect);
            pieChartData.Add(new()
            {
                Data = value,
                Label = text,
                LabelFrame = frameRect,
            });

            maxExplodeOffset = Math.Max(Math.Max(maxExplodeOffset, frameRect.Width), frameRect.Height);
        }
        explodeOffset = maxExplodeOffset;
    }

    private readonly struct PieChartData
    {
        public I1DData Data { get; init; }
        public string Label { get; init; }
        public SKRect LabelFrame { get; init; }
        public float CrossDistance { get; init; }
    }

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds)
    {
        SKPoint center = new(bounds.MidX, bounds.MidY);
        float radius = Math.Min(bounds.Width / 2, bounds.Height / 2) - explodeOffset;
        SKRect rect = new(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius);

        float startAngle = 0;

        foreach (var item in pieChartData)
        {
            float sweepAngle = 360f * item.Data.Percentage;

            using SKPath path = new();
            using SKPaint fillPaint = new()
            {
                Style = SKPaintStyle.Fill,
                Color = item.Data.Color
            };

            path.MoveTo(center);
            path.ArcTo(rect, startAngle, sweepAngle, false);
            path.Close();

            var frameRect = item.LabelFrame;

            float angle = startAngle + 0.5f * sweepAngle;
            float x = radius * (float)Math.Cos(Math.PI * angle / 180);
            float y = radius * (float)Math.Sin(Math.PI * angle / 180);

            x += x > 0 ? frameRect.Width / 2 : -frameRect.Width / 2;
            y += y > 0 ? frameRect.Height / 2 : -frameRect.Height / 2;

            textPaint.TextAlign = x > 0 ? SKTextAlign.Left : SKTextAlign.Right;

            canvas.DrawPath(path, fillPaint);
            canvas.DrawPath(path, outlinePaint);

            var textX = rect.MidX + x;
            var textY = rect.MidY + y;
            canvas.DrawText(item.Label, textX, textY, textPaint);

            startAngle += sweepAngle;
        }
    }

    public void Dispose()
    {
        outlinePaint.Dispose();
        textPaint.Dispose();
        textBackgroundPaint.Dispose();
        GC.SuppressFinalize(this);
    }
}
