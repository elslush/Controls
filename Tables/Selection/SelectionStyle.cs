using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Selection
{
    public enum SelectionStyle
    {
        /// <summary>No styling will be added when selected</summary>
        None,
        /// <summary>No styling will be added when selected</summary>
        Disabled,
        /// <summary>Selections will be highlighted.</summary>
        Highlight,
        /// <summary>Selections will trigger a radio button.</summary>
        Radio,
        /// <summary>Selections will trigger a check box.</summary>
        Checkbox,
        /// <summary>Selections will trigger a slide toggle.</summary>
        SlideToggle,
    }
}
