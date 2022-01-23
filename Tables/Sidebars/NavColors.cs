using Controls.CssStates;

namespace Controls.Sidebars
{
    public readonly struct NavColors
    {
        public CssStyle DefaultColors { get; init; }

        public CssStyle HoverColors { get; init; }

        public CssStyle ClickedColors { get; init; }
    }
}
