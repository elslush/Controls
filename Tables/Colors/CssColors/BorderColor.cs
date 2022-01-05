using System.Drawing;

namespace Controls.Colors.CssColors
{
    public readonly struct BorderColor : ICssColor
    {
        private const string cssName = "border-color";

        public BorderColor(Color color) => Css = $"{cssName}:{color.ToRGBStyle()};";

        public string Css { get; }
    }
}
