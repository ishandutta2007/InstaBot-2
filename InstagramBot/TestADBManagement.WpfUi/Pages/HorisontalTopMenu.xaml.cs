using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestADBManagement.WpfUi.Pages
{
    /// <summary>
    /// Interaction logic for HorisontalTopMenu.xaml
    /// </summary>
    public partial class HorisontalTopMenu : Page
    {
        private static MainWindow GetRunningMainWindow() => Application.Current.Windows[0] as MainWindow;

        public string AccountName { get; set; }
        public HorisontalTopMenu()
        {
            InitializeComponent();
            AccountName = GetRunningMainWindow().AccountName;
            mainGrid.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>
            GetRunningMainWindow().contentArea.Source = new Uri("Pages/Content/AddTask.xaml", UriKind.Relative);

        private void Guide_Button_Click(object sender, RoutedEventArgs e) => 
            GetRunningMainWindow().contentArea.Source = new Uri("Pages/Guide.xaml", UriKind.Relative);
    }
}
