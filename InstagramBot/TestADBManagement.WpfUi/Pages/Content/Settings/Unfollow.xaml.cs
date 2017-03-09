using InstaSharp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using TestADBManagement.WpfUi.Models;
using TestADBManagement.WpfUi.VM;

namespace TestADBManagement.WpfUi.Pages.Content.Settings
{
    /// <summary>
    /// Interaction logic for Unfollow.xaml
    /// </summary>
    public partial class Unfollow : Page
    {
        private const string USER_AGENT =
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/45.0.2414.0 Safari/537.36";

        public Unfollow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //InstagramApiController api = new InstagramApiController();
            //api.Autheticate("test_account_brooler", "TestPassword");

            //var client = new RestClient("https://www.instagram.com/");

            //client.CookieContainer = new CookieContainer();
            //client.UserAgent = USER_AGENT;

            //var firstRequest = new RestRequest("/", Method.GET);
            //var firstResponse = client.Execute(firstRequest);

            //var csrftoken = firstResponse.Cookies.First(x => x.Name == "csrftoken").Value;

            //var loginRequest = new RestRequest("/accounts/login/ajax/", Method.POST);
            //loginRequest.AddHeader("X-CSRFToken", csrftoken);
            //loginRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            //loginRequest.AddHeader("X-Instagram-AJAX", "1");
            //loginRequest.AddHeader("Referer", client.BaseUrl.ToString());
            //loginRequest.AddParameter("username", "test_account_brooler");
            //loginRequest.AddParameter("password", "TestPassword");

            //var loginResponse = client.Execute<LoginResponse>(loginRequest).Data;

            //var newResponse = client.Execute<LoginResponse>(loginRequest);

            //var userRequest = new RestRequest("/users/self", Method.GET);
            //userRequest.AddHeader("Referer", client.BaseUrl.ToString());

            //var userResponse = client.Execute<object>(userRequest);

            //var folowRequest = new RestRequest("/query", Method.POST);
            //folowRequest.AddHeader("referer", "https://www.instagram.com/test_account_brooler/");
            //folowRequest.AddHeader("X-CSRFToken", csrftoken);
            //folowRequest.AddHeader("X-Instagram-AJAX", "1");
            //folowRequest.AddHeader("x-requested-with", "XMLHttpRequest");
            //folowRequest.AddParameter("q", "ig_user(3236268191) { followed_by.first(10) { count, page_info { end_cursor, has_next_page }, nodes { id, is_verified, followed_by_viewer, requested_by_viewer, full_name, profile_pic_url, username } } }");
            //folowRequest.AddParameter("ref", "relationships::follow_list");

            //var response = client.Execute(folowRequest);

            //MessageBox.Show(newResponse.ToString());

            //var scopes = new List<OAuth.Scope>();
            //scopes.Add(InstaSharp.OAuth.Scope.Likes);
            //scopes.Add(InstaSharp.OAuth.Scope.Comments);

            //var config = new InstagramConfig("b68377a3d2794d2c8cc2a63a29209ada", "aa455874beb24fffbb4b1b35baeaf887");

            //var link = InstaSharp.OAuth.AuthLink(config.OAuthUri + "authorize", config.ClientId, config.RedirectUri, scopes, InstaSharp.OAuth.ResponseType.Code);
            //MessageBox.Show(link);

            //var client = new RestClient("https://api.instagram.com/");
            //var request = new RestRequest("/v1/users/self/follows", Method.GET);
            //request.AddParameter("access_token", "3236268191.b68377a.34bc58d0e7ab4cb7be88322863218486");

            //var response = client.Execute(request);
            if (Validation())
            {
                var vm_account = ((Application.Current.Windows[0] as MainWindow).accountsArea.Content as AccountsView).AccountsView_ListView.SelectedItem as VM_Account;
                var db = new InstagramDataContext();
                var account = db.InstagramAccounts.First(m => m.AccountName == vm_account.Title);

                var unfollow = new BotTasks.Unfollow();
                unfollow.StartTask(account, followersInput.Text.Split(','));
            }


        }

        private bool Validation()
        {
            if(((Application.Current.Windows[0] as MainWindow).accountsArea.Content as AccountsView).AccountsView_ListView.SelectedItem as VM_Account == null)
            {
                MessageBox.Show("Select account or add new one", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if ((((Application.Current.Windows[0] as MainWindow).accountsArea.Content as AccountsView).AccountsView_ListView.SelectedItem as VM_Account).AccountId == 0)
            {
                MessageBox.Show("Select account or add new one", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (followersInput.Text == "")
            {
                MessageBox.Show("Input account name(s)", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


    }
}
