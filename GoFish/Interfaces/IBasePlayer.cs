using System.Collections.Generic;
using CardLibrary;

namespace Interfaces
{
    public interface IBasePlayer
    {
        SelectOpponentDelegate SelectOpponentsCallback { get; set; }
        SelectCardValueDelegate SelectCardValueCallback { get; set; }
        ShowMessageDelegate ShowMessageCallback { get; set; }
        CardExchangeAnnouncementDelegate CardExchangeAnnouncementCallback { get; set; }

        List<Card> OnTheTable { get; }
        int CardsLeftOnHand { get; }
        CardController CurrentDeck { get; set; }
        IEnumerable<IBasePlayer> Opponents { get; set; }
        string PlayerName { get; }
        PlayerTypes PlayerType { get; }
        bool Play();
        IEnumerable<Card> GetCards(Values value);
        void OtherPlayersPlayed(IBasePlayer cardReciever, IBasePlayer cardSender, Values cardValue, IEnumerable<Card> cardsReceived);
    }
}
