using Controls.Charts.Base;
using SkiaSharp;

namespace Controls.Charts.Legends.LegendElements;

public interface ILegendElement : IDrawable
{
    public float GetTextSize(SKRect bounds);

    public void SetTextSize(float textSize);
}
