using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;
using Interfaces;

namespace PlayerLibrary.Tests
{
    class DummyViewModel : IGenericViewModel
    {
        public Values SelectCardValue(IEnumerable<Card> hand) => Values.Ess;
        public IBasePlayer SelectOpponent(IEnumerable<IBasePlayer> opponents) => null;
        public void ShowMessage(string message) => Debug.WriteLine(message);
    }
}
