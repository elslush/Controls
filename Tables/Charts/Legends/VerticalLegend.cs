using Controls.Charts.Base;
using Controls.Charts.Legends.LegendElements;
using SkiaSharp;

namespace Controls.Charts.Legends;

public readonly record struct VerticalLegend : IDrawable
{
    private readonly ICollection<ILegendElement> legendsElements;

    public VerticalLegend(ICollection<ILegendElement> legendsElements)
    {
        this.legendsElements = legendsElements;
    }

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds)
    {
        var elementHeight = 50;
        var elementBottom = bounds.Top;

        var initialBounds = new SKRect(bounds.Left, elementBottom, bounds.Right, elementBottom + elementHeight);
        var textSize = legendsElements.Select(element => element.GetTextSize(initialBounds)).Min();

        foreach (var legendElement in legendsElements)
        {
            legendElement.SetTextSize(textSize);
            var elementBounds = new SKRect(bounds.Left, elementBottom, bounds.Right, elementBottom += elementHeight);
            legendElement.Draw(ref canvas, ref info, elementBounds);
        }
    }
}
