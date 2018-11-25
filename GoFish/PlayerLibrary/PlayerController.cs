using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLibrary
{
    public static class PlayerController
    {
        public static IEnumerable<Type> GetPlayers()
        {
            return Assembly.GetAssembly(typeof(BasePlayer)).GetTypes().Where(theType => theType.IsSubclassOf(typeof(BasePlayer)));
        }
    }
}
