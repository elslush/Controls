namespace Controls.FloatingUI;

public readonly struct Strategy
{
    private readonly string value;

    private Strategy(string value) => this.value = value;

    public override string ToString() => value;

    public static implicit operator string(Strategy strategy) { return strategy.value; }

    static Strategy()
    {
        Absolute = new("absolute");
        Fixed = new("fixed");
    }

    public static Strategy Absolute { get; }
    public static Strategy Fixed { get; }
}
