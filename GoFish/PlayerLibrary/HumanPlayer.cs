using System;
using System.Linq;
using CardLibrary;
using Interfaces;

namespace PlayerLibrary
{
    public class HumanPlayer : BasePlayer
    {
        BasePlayer opponent;

        /// <summary>
        /// Empty constructor needs to exist to be able to create an object without injecting a deck
        /// </summary>
        public HumanPlayer() : base()
        { }

        /// <summary>
        /// Sets the players deck when called upon. See further in <see cref="CurrentDeck"/>
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public HumanPlayer(CardController currentDeck) : base(currentDeck)
        {
            PlayerType = PlayerTypes.Human;
        }

        public override BasePlayer SelectOpponent()
        {
            IBasePlayer temp = SelectOpponentsCallback?.Invoke(Opponents);
            opponent = Opponents.FirstOrDefault(o => o.PlayerName == temp.PlayerName);
            return opponent;
        }

        public override Values SelectValueToAskFor()
        {
            if (SelectCardValueCallback != null)
                return SelectCardValueCallback.Invoke(Hand);
            else return Values.Noll;
        }
    }
}
