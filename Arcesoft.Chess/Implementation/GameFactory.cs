using Arcesoft.Chess.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.Implementation
{
    internal class GameFactory : IGameFactory
    {
        private readonly FactoryContainer _container;

        public GameFactory(FactoryContainer container)
        {
            _container = container;
        }
        public IGame NewGame()
        {
            return _container.GetInstance<IGame>();
        }
    }
}
