using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GoFish
{
    class Program
    {
        static void Main(string[] args)
        {
            Startmenu startmenu = Startmenu.Create();
            startmenu.Run();
        }
    }
}
