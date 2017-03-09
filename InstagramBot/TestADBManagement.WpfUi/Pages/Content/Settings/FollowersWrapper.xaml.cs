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
using TestADBManagement.WpfUi.VM;
using TestADBManagement.WpfUi.VM.Settings;

namespace TestADBManagement.WpfUi.Pages.Content.Settings
{
    /// <summary>
    /// Interaction logic for FollowersWrapper.xaml
    /// </summary>
    public partial class FollowersWrapper : Page
    {
        public string AccountName { get; set; }
        public FollowersWrapper()
        {
            InitializeComponent();
            mainGrid.DataContext = this;
            var view = (Application.Current.Windows[0] as MainWindow).accountsArea.Content as AccountsView;
            if (view.AccountsView_ListView.SelectedItem != null && (view.AccountsView_ListView.SelectedItem as VM_Account).AccountId != 0)
            {
                AccountName = (view.AccountsView_ListView.SelectedItem as VM_Account).Title;
            }
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if(accountNameInput.Text=="")
            {
                MessageBox.Show("Enter account name first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int count;
            if(!int.TryParse(followersCountInput.Text, out count))
            {
                MessageBox.Show("Enter valid number of follows.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var wrapper = new BotTasks.FollowersWrapper(false);
            wrapper.StartWrapping(accountNameInput.Text.Split(','), count);

        }
    }
}
