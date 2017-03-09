using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TestADBManagement.WpfUi.Pages
{
    /// <summary>
    /// Interaction logic for AccountsView.xaml
    /// </summary>
    public partial class AccountsView : Page
    {
        readonly InstagramDataContext db = new InstagramDataContext();

        #region Accounts
        private ObservableCollection<VM_Account> _accounts;
        public ObservableCollection<VM_Account> Accounts
        {
            get { return _accounts == null ? _accounts = GetAccounts() : _accounts; }
        }

        private static ObservableCollection<VM_Account> GetAccounts()
        {
            var ret = new ObservableCollection<VM_Account>
            {
                new VM_Account { AccountId = 0, Title = "Add new account", Status="Instagram", ImageSource = "/Sources/add-account.png" }
            };

            // feeding some fake data
            ret.Add(new VM_Account { AccountId = 1, Title = "Дейви Джонс", Status = "Instagram", ImageSource = "/Sources/add-account.png" });
            ret.Add(new VM_Account { AccountId = 2, Title = "Товарищ Сталин", Status = "Instagram", ImageSource = "/Sources/add-account.png" });

            //var ids = System.IO.File.ReadAllLines("accounts.txt");
            //foreach (string id in ids)
            //{
            //    if (id != "0")
            //    {
            //        var account = db.InstagramAccounts.Find(int.Parse(id));
            //        ret.Add(
            //            new VM_Account
            //            {
            //                AccountId = account.Id,
            //                Title = account.AccountName,
            //                ImageSource = "/Sources/likes_wrap.png"
            //            });
            //    }
            //}

            return ret;
        }

        #endregion

        public AccountsView()
        {
            InitializeComponent();
        }

        private VM_Account GetSelectedAccount() => AccountsView_ListView.SelectedItem as VM_Account;

        private void AccountsView_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = GetSelectedAccount();
            if (item.AccountId == 0)
            {
                Application.Current.GetRunningMainWindow().GoToPage("Pages/Content/AddAccount.xaml");
            }
        }
    }
}
