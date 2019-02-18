using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Interfaces
{
    public delegate IBasePlayer SelectOpponentDelegate(IEnumerable<IBasePlayer> opponents);
    public delegate Values SelectCardValueDelegate(IEnumerable<Card> hand);
    public delegate void ShowMessageDelegate(string message);
}
