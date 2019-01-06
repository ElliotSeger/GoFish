using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLibrary
{
    public delegate void CardExchangeAnnouncement
        (
        BasePlayer cardReciever,
        BasePlayer cardSender,
        Values cardValue,
        IEnumerable<Card> returnResult
        );
}
