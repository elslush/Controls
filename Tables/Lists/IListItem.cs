using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controls.Selection;

namespace Controls.Lists
{
    public interface IListItem
    {
        public IListItem? Child { get; set; }

        public string SearchQuery { get; set; }

        public bool IsSelected { get; set; }

        public SelectionStyle SelectionStyle { get; set; }
    }
}
