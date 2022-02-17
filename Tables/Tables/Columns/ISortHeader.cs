using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Tables.Columns;

internal interface ISortHeader<TGridItem>
{
    internal IQueryable<TGridItem> GetSortedItems(IQueryable<TGridItem> source, bool ascending);
}
