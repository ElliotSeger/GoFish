﻿using System.Collections.Generic;
using System.Linq;
using CardLibrary;
using Interfaces;

namespace PlayerLibrary
{
    public class Player1 : BasePlayer
    {
        /// <summary>
        /// Empty constructor needs to exist to be able to create an object without injecting a deck
        /// </summary>
        public Player1() : base()
        { }

        /// <summary>
        /// Sets the players deck when called upon. See further in <see cref="CurrentDeck"/>
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public Player1(CardController currentDeck) : base(currentDeck) => PlayerType = PlayerTypes.Computer;

        //TODO! Make a strategic selection of opponent to ask for cards
        public override IBasePlayer SelectOpponent() =>
            // TODO! This code is experimental, needs to be rewritten
            // TODO! better way to pick an opponent
            Opponents.First();

        //TODO! Make a strategic selection of card value to ask for
        public override Values SelectValueToAskFor()
        {
            // TODO! add a way to choose what card to ask for, 
            // right now hardcoded to the value that is represented most
            Dictionary<Values, IEnumerable<Card>> valueGroups = Hand
                .GroupBy(key => key.Value, source => source, (key, cards) => new { Key = key, Value = cards })
                .OrderByDescending(s => s.Value.Count())
                .ToDictionary(g => g.Key, g => g.Value);
            // TODO! If valueGroups.Count() == 0 så är handen tom

            return valueGroups.First().Key;
        }
    }
}
