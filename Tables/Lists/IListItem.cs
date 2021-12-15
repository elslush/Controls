using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Lists
{
    public interface IListItem
    {
        public IListItem? Child { get; set; }

        public RenderFragment Render();

        public string SearchQuery { get; set; }
    }
}
