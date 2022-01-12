using System.Text;

namespace Controls.CssStates
{
    public readonly struct CssState
    {
        public CssState(params ICssValue[] colors) : this((IEnumerable<ICssValue>)colors) { }

        public CssState(IEnumerable<ICssValue> colors)
        {
            StringBuilder css = new();
            foreach (var color in colors)
                css.Append(color.Value);
            Value = css.ToString();
        }

        public string Value { get; }
    }
}
