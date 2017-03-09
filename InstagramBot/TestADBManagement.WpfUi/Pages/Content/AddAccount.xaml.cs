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
using TestADBManagement.BotTasks.Models;
using TestADBManagement.WpfUi.VM;

namespace TestADBManagement.WpfUi.Pages.Content
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Page
    {
        private static void LogAccountsIDsToTxtFile(IEnumerable<VM_Account> accounts)
        {
            var list = new List<string>();
            foreach (var id in accounts.Select(m => m.AccountId))
            {
                list.Add(id.ToString());
            }

            System.IO.File.WriteAllLines("accounts.txt", list.ToArray());
        }

        private static void UpdateDB(InstagramDataContext db, InstagramAccount account)
        {
            if (db.InstagramAccounts.FirstOrDefault(m => m.AccountName == account.AccountName) == null)
            {
                db.InstagramAccounts.Add(account);
                db.SaveChanges();
            }
        }

        private static void UpdateUI(InstagramDataContext instagramDC, string accountName) =>
            GetAccountsList().Add
                (new VM_Account
                    {
                        AccountId = instagramDC.InstagramAccounts.First(m => m.AccountName == accountName).Id,
                        Title = accountName,
                        ImageSource = "/Sources/likes_wrap.png"
                    });

        private static IList<VM_Account> GetAccountsList() =>
            (Application.Current.GetRunningMainWindow().accountsArea.Content as AccountsView).Accounts;

        public AddAccount()
        {
            InitializeComponent();
        }

        private void submitAccountForm_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                using (var db = new InstagramDataContext())
                {
                    var account = AssembleAccountFromForm();

                    UpdateDB(db, account);
                    UpdateUI(db, account.AccountName);
                    LogAccountsIDsToTxtFile(GetAccountsList());
                }         
            }
         }

        private InstagramAccount AssembleAccountFromForm() => 
            new InstagramAccount
                {
                    AccountName = AccountName_TextBox.Text,
                    InstagramPass = Password_TextBox.Text,
                    Email = Email_TextBox.Text
                };

        private bool Validation()
        {
            if (AccountName_TextBox.Text == "")
            {
                MessageBox.Show("Enter account name first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Password_TextBox.Text == "")
            {
                MessageBox.Show("Enter password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if(Email_TextBox.Text == "")
            {
                MessageBox.Show("Enter email", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
