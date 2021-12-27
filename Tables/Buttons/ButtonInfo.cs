using System.Drawing;
using static Controls.ColorPickers.ColorExtensions;

namespace Controls.Buttons
{
    public readonly struct ButtonInfo
    {
        private static readonly Color defaultBackground = Color.FromArgb(94, 164, 228),
            defaultText = Color.White;
        private static readonly ButtonInfo defaultButtonInfo = new(null, null, null, null, null, null);

        public static ButtonInfo Default => defaultButtonInfo;

        public static Color DefaultBackground => defaultBackground;

        public static Color DefaultText => defaultText;

        public ButtonInfo(Color? backGround, Color? backgroundHover, Color? text, Color? textHover, Color? clicked, Color? clickedHover)
        {
            Color backgroundColor = backGround is null ? defaultBackground : (Color)backGround;
            Color textColor = text is null ? defaultText : (Color)text;

            Background = backgroundColor.ToHslStyle();
            BackgroundHover = backgroundHover is null ? backgroundColor.ToDarkerHslStyle() : ((Color)backgroundHover).ToHslStyle();
            Text = textColor.ToHslStyle();
            TextHover = textHover is null ? textColor.ToDarkerHslStyle() : ((Color)textHover).ToHslStyle();
            Clicked = clicked is null ? BackgroundHover : ((Color)clicked).ToHslStyle();
            ClickedHover = clickedHover is null ? TextHover : ((Color)clickedHover).ToHslStyle();
        }

        public string Background { get; }

        public string BackgroundHover { get; }

        public string Text { get; }

        public string TextHover { get; }

        public string Clicked { get; }

        public string ClickedHover { get; }
    }
}
