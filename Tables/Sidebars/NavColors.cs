using Controls.CssStates;

namespace Controls.Sidebars
{
    public readonly struct NavColors
    {
        public CssState DefaultColors { get; init; }

        public CssState HoverColors { get; init; }

        public CssState ClickedColors { get; init; }
    }
}
