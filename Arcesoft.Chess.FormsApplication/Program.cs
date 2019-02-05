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

            Application.ThreadException += Application_ThreadException;

            var mainForm = container.GetInstance<FormMain>();
            Application.Run(mainForm);// new FormMain(container));

        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var chessException = e.Exception as ChessException;

            if (chessException != null)
            {
                MessageBox.Show(e.Exception.Message, "Invalid command", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Oops...an unexpected error has occurred.", "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }         
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
