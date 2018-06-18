using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcesoft.Chess.DependencyInjection
{
    /// <summary>
    /// Defines a common interface for container registrations
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Bind dependencies for given container
        /// </summary>
        /// <param name="container"></param>
        void BindDependencies(Container container);
    }
}
