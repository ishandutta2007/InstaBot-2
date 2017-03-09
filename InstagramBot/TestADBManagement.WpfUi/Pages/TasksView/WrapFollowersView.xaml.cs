using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TestADBManagement.WpfUi.Models;

namespace TestADBManagement.WpfUi.Pages.TasksView
{
    /// <summary>
    /// Interaction logic for WrapFollowersView.xaml
    /// </summary>
    public partial class WrapFollowersView : UserControl
    {
        public WrapFollowersView()
        {
            InitializeComponent();
        }


        #region Validation

        /// Regex down below expects something like: 
        /// http://www.instagram.com/account
        /// www.instagram.com / account
        /// instagram.com / account
        private readonly Regex _linkToAccountValidator = new Regex(@"(http://|https://){0,1}(www.){0,1}instagram\.com/([a-zA-Z0-9]+)");

        private bool Validate() => 
            ValidateLinkToAccount() && 
            ValidateInterval() &&
            ValidateTime();

        private bool ValidateLinkToAccount() => _linkToAccountValidator.IsMatch(LinkToAccount_TextBox.Text);

        private bool ValidateInterval()
        {
            double min, max = default(double);

            if (!double.TryParse(MinInterval_TextBox.Text, out min) | !double.TryParse(MaxInterval_TextBox.Text, out max)) return false;

            var validationLogic = new Func<double, double, bool>(
                (min_double, max_double) =>
                {
                    return min_double > 0 && min_double < max_double;
                });

            return validationLogic(min, max);
        }

        private bool ValidateTime()
        {
            DateTime from, to = default(DateTime);

            if (!DateTime.TryParse(FromTime_TextBox.Text, out from) | !DateTime.TryParse(ToTime_TextBox.Text, out to)) return false;

            var validationLogic = new Func<DateTime, DateTime, bool>(
                (from_dateTime, to_dateTime) =>
                {
                    return from_dateTime.Hour < to_dateTime.Hour;
                });

            return validationLogic(from, to);
        }
        #endregion

        private void SubmitForm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var newTaskParameters = AssembleParameters();

                // do something with VALID newTaskParameters here
            }
            else MessageBox.Show("Something wrong with input data!", "Error!", MessageBoxButton.OK);
        }

        private WrapFollowersTaskParameters AssembleParameters() =>
            new WrapFollowersTaskParameters
            {
                LinkToAccount = LinkToAccount_TextBox.Text,
                MinInterval = double.Parse(MinInterval_TextBox.Text),
                MaxInterval = double.Parse(MaxInterval_TextBox.Text),
                FromTime = DateTime.Parse(FromTime_TextBox.Text),
                ToTime = DateTime.Parse(ToTime_TextBox.Text)
            };
    }
}
