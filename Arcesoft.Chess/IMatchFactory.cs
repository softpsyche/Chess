using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess
{
    public interface IMatchFactory
    {
        List<Match> Load(string pgnString);
    }
}
