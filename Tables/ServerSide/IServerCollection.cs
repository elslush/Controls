using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.ServerSide;

public interface IServerCollection<T>
{
    public int Count { get; set; }

    public IEnumerable<T> Items { get; set; }
}