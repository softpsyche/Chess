using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arcesoft.Chess.FormsApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(container));
        }

        public static Container BuildContainer()
        {
            var container = new Container();
            container.Options.AllowOverridingRegistrations = true;

            new Chess.DependencyInjection.Binder().BindDependencies(container);
            new ArtificialIntelligence.DependencyInjection.Binder().BindDependencies(container);

            return container;
        }
    }
}
