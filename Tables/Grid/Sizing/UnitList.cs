using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class UnitList : IColumns
    {
        private readonly IUnit[] units;

        public UnitList(params IUnit[] units) => this.units = units;

        public override string ToString() => string.Join(" ", units.Select(unit => unit.ToString()));
    }
}
