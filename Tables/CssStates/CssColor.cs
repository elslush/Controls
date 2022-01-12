using System.Drawing;
using static Controls.Colors.ColorExtensions;

namespace Controls.CssStates
{
    public readonly struct CssColor : ICssValue
    {
        public CssColor(string name, Color color)
        {
            Value = $"{name}:{color.ToRGBStyle()};";
        }
        public string Value { get; }
    }
}
