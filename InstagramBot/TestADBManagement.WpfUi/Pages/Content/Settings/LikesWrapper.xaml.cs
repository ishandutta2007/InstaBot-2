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

namespace TestADBManagement.WpfUi.Pages.Content.Settings
{
    /// <summary>
    /// Interaction logic for LikesWrapper.xaml
    /// </summary>
    public partial class LikesWrapper : Page
    {
        public LikesWrapper()
        {
            InitializeComponent();
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (postLinkInput.Text == "")
            {
                MessageBox.Show("Enter account name first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int count;
            if (!int.TryParse(likesCountInput.Text, out count))
            {
                MessageBox.Show("Enter valid number of follows.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var wrapper = new BotTasks.LikesWrapper();
            wrapper.StartWrapping(postLinkInput.Text.Split(','), count);
        }
    }
}
