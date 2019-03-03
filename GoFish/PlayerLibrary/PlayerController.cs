using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Interfaces;

namespace PlayerLibrary
{
    //playercontroller följer factorypattern.
    public class PlayerController
    {
        private readonly Dictionary<string, Type> players = new Dictionary<string, Type>();
        private readonly IGenericViewModel viewModel;

        public PlayerController(IGenericViewModel viewModel)
        {
            this.viewModel = viewModel;
            foreach (Type item in Assembly.GetAssembly(typeof(BasePlayer)).GetTypes().Where(theType => theType.IsSubclassOf(typeof(BasePlayer))))
            {
                players.Add(item.Name, item);
            }
        }

        public IEnumerable<string> GetPlayers() => players.Keys;

        public BasePlayer InstantiatePlayer(string name)
        {
            BasePlayer result = (BasePlayer)Activator.CreateInstance(players[name]);
            result.SelectOpponentsCallback += viewModel.SelectOpponent;
            result.SelectCardValueCallback += viewModel.SelectCardValue;
            result.ShowMessageCallback += viewModel.ShowMessage;
            return result;
        }

    }
}
