namespace Controls.Css;

public readonly struct Styles
{
    private readonly string css;

    public Styles(params Style[] styles) : this((IEnumerable<Style>)styles) { }

    public Styles(IEnumerable<Style> styles) => css = string.Concat(styles);

    private Styles(string css) => this.css = css;

    //public static implicit operator string(Styles styles) => styles.css;

    //public static explicit operator Styles(string css) => new(css);

    public override string ToString() => css;
}