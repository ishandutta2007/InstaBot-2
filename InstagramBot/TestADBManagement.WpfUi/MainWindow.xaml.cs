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
using TestADBManagement.Auth;

namespace TestADBManagement.WpfUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string AccountName { get; set; }
        public string Password { get; set; }

        public MainWindow(string accountName)
        {
            InitializeComponent();
            AccountName = accountName;
                
        }
        public MainWindow()
        {
            AccountName = System.IO.File.ReadAllLines("authInfo.txt")[0];
            Password = System.IO.File.ReadAllLines("authInfo.txt")[1];

            AuthRequest();
        }

        public async void AuthRequest()
        {
            var auth = new Authentication();

            var result = await auth.AuthRequest(AccountName, Password);

            if (!result.result)  //Warning right - (!result.Result)
            {
                MessageBox.Show("Something went wrong with authetication!\nTry to login again", "Auth error", MessageBoxButton.OK, MessageBoxImage.Warning);
                var welcomeWindow = new WelcomeWindow();
                welcomeWindow.Show();
                this.Close();
            }
        }

        public void GoToPage(string pathToPage) => contentArea.Source = new Uri(pathToPage, UriKind.Relative);
    }
}
