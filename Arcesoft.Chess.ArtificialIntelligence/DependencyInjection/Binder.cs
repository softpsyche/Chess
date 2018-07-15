using Arcesoft.Chess.ArtificialIntelligence.Implementation;
using Arcesoft.Chess.Implementation;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.ArtificialIntelligence.DependencyInjection
{
    public class Binder
    {
        public void BindDependencies(Container container)
        {
            //general
            container.Register<IScoreCalculator, ScoreCalculator>();
            container.Register<IArtificialIntelligence, MiniMaxArtificialIntelligence>();
        }
    }
}
