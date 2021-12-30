using System.Drawing;
using System.Text;
using static Controls.ColorPickers.ColorExtensions;

namespace Controls.Selection
{
    public readonly struct SelectionColors
    {
        public ColorState StaticColors { get; init; } = ColorState.Default;

        public ColorState HoverColors { get; init; } = ColorState.Empty;

        public ColorState ClickedColors { get; init; } = ColorState.Empty;

        public ColorState DisabledColors { get; init; } = ColorState.Empty;
    }

    public readonly struct ColorState
    {
        private static readonly Color defaultBackgroundColor = Color.FromArgb(94, 164, 228);
        private static readonly ColorState defaultColorState = new(defaultBackgroundColor, Color.White);
        private static readonly ColorState disabledColorState = new(Color.Gray, Color.DarkGray);
        private static readonly ColorState emptyColorState = new();
        public static ColorState Default => defaultColorState;
        public static Color DefaultBackgroundColor => defaultBackgroundColor;
        public static ColorState Empty => emptyColorState;
        public static ColorState Disabled => disabledColorState;

        public ColorState(string background = "", string text = "", string border = "")
        {
            StringBuilder stringBuilder = new();
            if (!string.IsNullOrWhiteSpace(background))
                stringBuilder.Append($"background-color: {background};");
            if (!string.IsNullOrWhiteSpace(text))
                stringBuilder.Append($"color: {text};");
            if (!string.IsNullOrWhiteSpace(border))
                stringBuilder.Append($"border-color: {border};");
            Styles = stringBuilder.ToString();
        }

        public ColorState(Color? background = null, Color? text = null, Color? border = null)
        {
            StringBuilder stringBuilder = new();
            if (background is not null)
                stringBuilder.Append($"background-color: {((Color)background).ToRGBStyle()};");
            if (text is not null)
                stringBuilder.Append($"color: {((Color)text).ToRGBStyle()};");
            if (border is not null)
                stringBuilder.Append($"border-color: {((Color)border).ToRGBStyle()};");
            Styles = stringBuilder.ToString();
        }

        public string Styles { get; }
    }
}
