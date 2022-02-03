using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Colors
{
    public static class ColorExtensions
    {
        public static string ToHslStyle(this Color color) => $"hsl({color.GetHue()}, {color.GetSaturation() * 100}%, {color.GetBrightness() * 100}%)";

        public static string ToRGBStyle(this Color color) => $"rgba({color.R}, {color.G}, {color.B}, {color.A})";

        public static Color GetContrastingTextColor(this Color color) => color.ApproximateLuminance() > 0.1791 ? Color.Black : Color.White;

        public static double CalcluateContrastRatio(this Color color, Color other) => (color.ApproximateLuminance() + 0.05) / (other.ApproximateLuminance() + 0.05);

        public static double ApproximateLuminance(this Color color) => (color.R + color.R + color.R + color.B + color.G + color.G + color.G + color.G) >> 3;
    }
}
