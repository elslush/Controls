using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Css
{
    public interface ICustomizable
    {
        public Classes? CssClasses { get; set; }

        public Styles? CssStyles{ get; set; }
    }
}
