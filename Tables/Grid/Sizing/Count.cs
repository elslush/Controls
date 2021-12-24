using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class Count : IRepeatCount
    {
        private readonly int count;

        public Count(int count) => this.count = count;

        public override string ToString() => count.ToString();
    }
}
