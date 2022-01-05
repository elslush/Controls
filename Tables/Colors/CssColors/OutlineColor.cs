using System.Drawing;

namespace Controls.Colors.CssColors
{
    public readonly struct OutlineColor : ICssColor
    {
        private const string cssName = "outline-color";

        public OutlineColor(Color color) => Css = $"{cssName}:{color.ToRGBStyle()};";

        public string Css { get; }
    }
}
