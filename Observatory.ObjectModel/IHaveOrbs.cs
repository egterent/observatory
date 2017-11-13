using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observatory.ObjectModel
{
    public interface IHaveOrbs
    {
        ICollection OrbsCollection { get; }
    }
}
