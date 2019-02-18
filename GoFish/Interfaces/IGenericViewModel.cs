using System.Collections.Generic;
using CardLibrary;

namespace Interfaces
{
    public interface IGenericViewModel
    {
        IBasePlayer SelectOpponent(IEnumerable<IBasePlayer> opponents);
        Values SelectCardValue(IEnumerable<Card> hand);
        void ShowMessage(string message);
    }
}
