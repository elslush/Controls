using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Animations
{
    public interface IAnimation
    {
        public string Class { get; }

        public IEnumerable<string> Variables { get; }
    }
}
