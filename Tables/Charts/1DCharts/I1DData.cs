using SkiaSharp;

namespace Controls.Charts._1DCharts;

public interface I1DData
{
    public string Label { get; set; }

    public float Percentage { get; set; }

    public float RawValue { get; set; }

    public SKColor Color { get; set; }
}
