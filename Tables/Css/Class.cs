namespace Controls.Css;

public readonly struct Class
{
    private readonly string cssClass;
    public Class(string cssClass) => this.cssClass = cssClass;

    //public static implicit operator string(Class style) => style.cssClass;

    //public static explicit operator Class(string cssClass) => new(cssClass);

    public override string ToString() => cssClass;
}