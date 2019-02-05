using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arcesoft.Chess.FormsApplication
{
    internal static class Extensions
    {
        public static void ToMessageBox(this Exception exception)
        {
            MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
