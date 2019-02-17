using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLibrary
{
    //playercontroller följer factorypattern.
    public class PlayerController
    {
        private Dictionary<string, Type> players = new Dictionary<string, Type>();

        public PlayerController(IGenericViewModel vm)
        {
            foreach (var item in Assembly.GetAssembly(typeof(BasePlayer)).GetTypes().Where(theType => theType.IsSubclassOf(typeof(BasePlayer))))
            {
                players.Add(item.Name, item);
            }
        }

        public IEnumerable<string> GetPlayers()
        {
            return players.Keys;
        }

        public BasePlayer InstantiatePlayer(string name)
        {
            return (BasePlayer)Activator.CreateInstance(players[name]);
        }


    }
}
