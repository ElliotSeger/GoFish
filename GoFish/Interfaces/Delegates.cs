using System.Collections.Generic;
using CardLibrary;

namespace Interfaces
{
    public delegate IBasePlayer SelectOpponentDelegate(IEnumerable<IBasePlayer> opponents);
    public delegate Values SelectCardValueDelegate(IEnumerable<Card> hand);
    public delegate void ShowMessageDelegate(string message);
    public delegate void CardExchangeAnnouncementDelegate(IBasePlayer cardReciever,
        IBasePlayer cardSender, Values cardValue, IEnumerable<Card> returnResult);
}
