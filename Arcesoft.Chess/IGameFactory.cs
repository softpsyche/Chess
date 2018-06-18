using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess
{
    public interface IGameFactory
    {
        IGame NewGame();
    }
}
