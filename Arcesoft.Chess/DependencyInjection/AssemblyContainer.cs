using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.DependencyInjection
{
    /// <summary>
    /// Use this class to register container singleton for your factory classes that might need to service locate.
    /// Inherit in your assembly so each type can be segregated to its own assembly 
    /// (avoids problems of multiple dlls dealing with re-registrations on common interface/type)
    /// </summary>
    public abstract class AssemblyContainer
    {
        private Container Container { get; set; }

        protected AssemblyContainer(Container container)
        {
            Container = container;
        }

        public T GetInstance<T>()
            where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}
