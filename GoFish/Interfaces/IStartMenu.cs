using System.Collections.Generic;

namespace Interfaces
{
    public interface IStartMenu
    {
        IEnumerable<IBasePlayer> Execute();
    }
}
