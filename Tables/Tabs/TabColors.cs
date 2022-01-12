using Controls.CssStates;

namespace Controls.Tabs
{
    public readonly struct TabColors
    {
        public CssState DefaultColors { get; init; }

        public CssState HoverColors { get; init; }

        public CssState ClickedColors { get; init; }
    }
}
