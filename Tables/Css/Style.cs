namespace Controls.Css;

public readonly struct Style
{
    private readonly string css;
    public Style(string name, string value) => css = $"{name}:{value};";

    private Style(string css) => this.css = css;

    //public static implicit operator string(Style style) => style.css;

    //public static explicit operator Style(string css) => new(css);

    public override string ToString() => css;
}