using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Charts._1DCharts.CircleCharts;

public class InnerCircleChartLabel : ICircleChartLabel
{
    public SKRect Bounds { get; set; }

    public void Draw(ref SKCanvas canvas, ref SKImageInfo info, SKRect bounds)
    {
        throw new NotImplementedException();
    }
}
