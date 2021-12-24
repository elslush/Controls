using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class Pixel : IUnit, IRows, IColumns, IGap, IRepeater
    {
        private readonly int value;

        public Pixel(int value) => this.value = value;

        public override string ToString() => $"{value}px";
    }
}
