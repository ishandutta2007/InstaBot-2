using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestADBManagement.WpfUi
{
    public static class ExtensionMethods
    {
        public static MainWindow GetRunningMainWindow(this Application @this) => @this.Windows[0] as MainWindow;
    }
}
