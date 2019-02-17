using PlayerLibrary;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IGenericViewModel
    {
        void ViewOpponent(IEnumerable<BasePlayer> opponents);
    }
}
