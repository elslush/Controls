namespace Controls.FloatingUI;

public readonly struct Placement 
{
    private readonly string value;

    private Placement(string value) => this.value = value;

    public override string ToString() => value;

    public static implicit operator string(Placement placement) { return placement.value; }

    static Placement()
    {
        Top = new("top");
        Bottom = new("bottom");
        Right = new("right");
        Left = new("left");
        TopStart = new("top-start");
        BottomStart = new("bottom-start");
        RightStart = new("right-start");
        LeftStart = new("left-start");
        TopEnd = new("top-end");
        BottomEnd = new("bottom-end");
        RightEnd = new("right-end");
        LeftEnd = new("left-end");
    }

    public static Placement Top { get; }
    public static Placement Bottom { get; }
    public static Placement Right { get; }
    public static Placement Left { get; }
    public static Placement TopStart { get; }
    public static Placement BottomStart { get; }
    public static Placement RightStart { get; }
    public static Placement LeftStart { get; }
    public static Placement TopEnd { get; }
    public static Placement BottomEnd { get; }
    public static Placement RightEnd { get; }
    public static Placement LeftEnd { get; }
}