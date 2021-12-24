using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Grid.Sizing
{
    public class Percentage : IUnit, IGap, IRepeater
    {
        private readonly int value;

        public Percentage(int value) => this.value = value;

        public override string ToString() => $"{value}%";
    }
}
