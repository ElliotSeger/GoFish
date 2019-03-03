using System.Collections.Generic;
using System.Diagnostics;
using CardLibrary;
using Interfaces;

namespace PlayerLibrary.Tests
{
    // Dummy ViewModel for test purpose that:
    // Always returns selected card as Values.Ess
    // Doesn't return any selected opponent
    // Doesn't generate any output
    // This is what is called a mock or fake object
    internal class DummyViewModel : IGenericViewModel
    {
        public Values SelectCardValue(IEnumerable<Card> hand) => Values.Ess;
        public IBasePlayer SelectOpponent(IEnumerable<IBasePlayer> opponents) => null;
        public void ShowMessage(string message) => Debug.WriteLine(message);
    }
}
