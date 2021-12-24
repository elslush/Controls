using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class AutoFit : IRepeatCount
    {
        private const string value = "auto-fit";

        public override string ToString() => value;
    }
}
