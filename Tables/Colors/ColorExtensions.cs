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

        public static string ToDarkerHslStyle(this Color color, double multiplier = 1.5) => $"hsl({color.GetHue()}, {color.GetSaturation() * 100}%, {color.GetBrightness() * 100 / multiplier}%)";
        public static string ToRGBStyle(this Color color) => $"rgb({color.R}, {color.G}, {color.B})";
    }
}
