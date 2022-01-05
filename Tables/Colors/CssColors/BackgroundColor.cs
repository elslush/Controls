using System.Drawing;

namespace Controls.Colors.CssColors
{
    public readonly struct BackgroundColor : ICssColor
    {
        private const string cssName = "background-color";

        public BackgroundColor(Color color) => Css = $"{cssName}:{color.ToRGBStyle()};";

        public string Css { get; }
    }
}
