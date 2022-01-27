namespace Controls.Css;

public readonly struct Classes
{
    private readonly string cssClasses;

    public Classes(params Class[] classes) : this((IEnumerable<Class>)classes) { }

    public Classes(IEnumerable<Class> classes) => cssClasses = string.Join(' ', classes);

    private Classes(string cssClass) => this.cssClasses = cssClass;

    //public static implicit operator string(Classes styles) => styles.cssClasses;

    //public static explicit operator Classes(string cssClass) => new(cssClass);

    public override string ToString() => cssClasses;
}