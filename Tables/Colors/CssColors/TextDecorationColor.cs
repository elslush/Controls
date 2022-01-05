using System.Drawing;

namespace Controls.Colors.CssColors
{
    internal readonly struct TextDecorationColor : ICssColor
    {
        private const string cssName = "text-decoration-color";

        public TextDecorationColor(Color color) => Css = $"{cssName}:{color.ToRGBStyle()};";

        public string Css { get; }
    }
}
