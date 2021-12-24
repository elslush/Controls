using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class MaxContent : IRepeater
    {
        private const string value = "max-content";

        public override string ToString() => value;
    }
}
