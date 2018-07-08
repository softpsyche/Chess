using Arcesoft.Chess.Implementation;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.DependencyInjection
{
    public class Binder : IBinder
    {
        public void BindDependencies(Container container)
        {
            //general
            container.RegisterInstance(new FactoryContainer(container));

            container.Register<IGameFactory, GameFactory>();
            container.Register<IGame, Game>();
            container.Register<IMatchFactory, MatchFactory>();
        }
    }
}
