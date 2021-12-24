using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class FitContent : IRepeater
    {
        private readonly IUnit unit;

        public FitContent(IUnit unit) => this.unit = unit;

        public override string ToString() => $"fit-content({unit})";
    }
}
