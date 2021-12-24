using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class MinContent : IRepeater
    {
        private const string value = "min-content";

        public override string ToString() => value;
    }
}
