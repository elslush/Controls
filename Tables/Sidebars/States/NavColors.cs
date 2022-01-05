using Controls.Colors;

namespace Controls.Sidebars.States
{
    public readonly struct NavColors
    {
        public ColorState DefaultColors { get; init; }

        public ColorState HoverColors { get; init; }

        public ColorState ClickedColors { get; init; }
    }
}
