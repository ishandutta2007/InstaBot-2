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
using TestADBManagement.BotTasks;
using TestADBManagement.BotTasks.Models;
using TestADBManagement.WpfUi.VM;

namespace TestADBManagement.WpfUi.Pages.Content.Settings
{
    /// <summary>
    /// Interaction logic for TargetLike.xaml
    /// </summary>
    public partial class TargetLike : Page
    {
        public string TargetHeader { get; set; }
        public TargetLike()
        {
            InitializeComponent();
            mainGrid.DataContext = this;
            var items = new List<string>
            {
                "Geoposition", "Hashtag"
            };
            targetTypeInput.ItemsSource = items;
            targetTypeInput.SelectedIndex = 0;
            TargetHeader = "Geoposition link:";
        }

        private void targetTypeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (targetTypeInput.SelectedItem as string)
            {
                case "Geoposition":
                    {
                        TargetHeader = "Geoposition link:";
                        targetHeader.Text = TargetHeader;
                        break;
                    }
                case "Hashtag":
                    {
                        TargetHeader = "Hashtag (without '#'):";
                        targetHeader.Text = TargetHeader;
                        break;
                    }
            }
        }



        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                switch (targetTypeInput.SelectedItem as string)
                {
                    case "Geoposition":
                        {
                            GeopositionLike();
                            break;
                        }
                    case "Hashtag":
                        {
                            HashtagLike();
                            break;
                        }
                }
            }
        }

        private bool Validation()
        {
            var view = (Application.Current.Windows[0] as MainWindow).accountsArea.Content as AccountsView;
            if (view.AccountsView_ListView.SelectedItem == null || (view.AccountsView_ListView.SelectedItem as VM_Account).AccountId == 0)
            {
                MessageBox.Show("Select or add account first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (targetInput.Text == "")
            {
                MessageBox.Show("Enter target link or hashtag", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            int count;
            if (!int.TryParse(likeCountInput.Text, out count))
            {
                MessageBox.Show("Enter valid number of likes you need", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void GeopositionLike()
        {
            var liker = new LocationLiker();
            var vm_account = ((Application.Current.Windows[0] as MainWindow).accountsArea.Content as AccountsView).AccountsView_ListView.SelectedItem as VM_Account;
            var db = new InstagramDataContext();
            var account = db.InstagramAccounts.First(m => m.AccountName == vm_account.Title);
            liker.StartLike(targetInput.Text, int.Parse(likeCountInput.Text), account);
        }

        private void HashtagLike()
        {
            var liker = new TagLiker();
            var vm_account = ((Application.Current.Windows[0] as MainWindow).accountsArea.Content as AccountsView).AccountsView_ListView.SelectedItem as VM_Account;
            var db = new InstagramDataContext();
            var account = db.InstagramAccounts.First(m => m.AccountName == vm_account.Title);
            liker.StartLike(targetInput.Text, int.Parse(likeCountInput.Text), account);
        }
        
    }
}
