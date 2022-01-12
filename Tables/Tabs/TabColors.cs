using Controls.Colors;

namespace Controls.Tabs
{
    public readonly struct TabColors
    {
        public ColorState DefaultColors { get; init; }

        public ColorState HoverColors { get; init; }

        public ColorState ClickedColors { get; init; }
    }
}
