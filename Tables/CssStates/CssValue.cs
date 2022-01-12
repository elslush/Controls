using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.CssStates
{
    public readonly struct CssValue : ICssValue
    {
        public CssValue(string name, string value)
        {
            Value = $"{name}:{value};";
        }
        public string Value { get; }
    }
}
