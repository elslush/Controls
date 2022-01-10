using System.Drawing;

namespace Controls.Colors.CssColors
{
    public readonly struct TextColor : ICssColor
    {
        private const string cssName = "color";

        public TextColor(Color color) => Css = $"{cssName}:{color.ToRGBStyle()};";

        public string Css { get; }
    }
}
