using Controls.Colors.CssColors;
using System.Text;

namespace Controls.Colors
{
    public readonly struct ColorState
    {
        public ColorState(params ICssColor[] colors) : this((IEnumerable<ICssColor>)colors) { }

        public ColorState(IEnumerable<ICssColor> colors)
        {
            StringBuilder css = new();
            foreach (var color in colors)
                css.Append(color.Css);
            Css = css.ToString();
        }

        public string Css { get; }
    }
}
