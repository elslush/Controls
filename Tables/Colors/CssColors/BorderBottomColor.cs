using System.Drawing;

namespace Controls.Colors.CssColors
{
    public readonly struct BorderBottomColor : ICssColor
    {
        private const string cssName = "border-bottom";

        public BorderBottomColor(Color color) => Css = $"{cssName}:{color.ToRGBStyle()};";

        public string Css { get; }
    }
}
