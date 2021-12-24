using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class Fractional : IUnit, IRepeater
    {
        private readonly int value;

        public Fractional(int value) => this.value = value;

        public override string ToString() => $"{value}fr";
    }
}
