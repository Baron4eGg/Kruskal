using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Алгоритм_Крускала
{
    public interface IKruskal
    {
        IList<Edge> Solve(IList<Edge> graph, out int totalCost);
    }
}
