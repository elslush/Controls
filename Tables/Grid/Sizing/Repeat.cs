using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class Repeat : IColumns
    {
        private readonly IRepeatCount repeatCount;
        private readonly IRepeater repeater;

        public Repeat(IRepeatCount repeatCount, IRepeater repeater)
        {
            this.repeatCount = repeatCount;
            this.repeater = repeater;
        }

        public override string ToString() => $"repeat({repeatCount},{repeater})";
    }
}
