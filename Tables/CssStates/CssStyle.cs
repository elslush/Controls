using System.Text;

namespace Controls.CssStates
{
    public readonly struct CssStyle
    {
        public CssStyle(params ICssValue[] colors) : this((IEnumerable<ICssValue>)colors) { }

        public CssStyle(IEnumerable<ICssValue> colors)
        {
            StringBuilder css = new();
            foreach (var color in colors)
                css.Append(color.Value);
            Value = css.ToString();
        }

        public string Value { get; }
    }
}
