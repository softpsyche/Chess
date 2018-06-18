using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.DependencyInjection
{

    internal class FactoryContainer : AssemblyContainer
    {
        public FactoryContainer(Container container) : base(container) { }
    }

}
