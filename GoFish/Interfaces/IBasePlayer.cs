using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBasePlayer
    {
        SelectOpponentDelegate SelectOpponentsCallback { get; set; }
        SelectCardValueDelegate SelectCardValueCallback { get; set; }
        ShowMessageDelegate ShowMessageCallback { get; set; }
        string PlayerName { get; }
        PlayerTypes PlayerType { get; }
    }
}
