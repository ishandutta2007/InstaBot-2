using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestADBManagement.Auth;

namespace TestADBManagement.WpfUi
{
    
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        private string userEmail;
        private string hashPass;
        private string sessionKey;
        private string licenseKey;
        private string programHash = ConfigurationManager.AppSettings["ProgramHash"];

        public WelcomeWindow()
        {
            InitializeComponent();
            loginInput.GotFocus += new RoutedEventHandler(RemoveText);
            loginInput.LostFocus += new RoutedEventHandler(AddText);
            keyInput.GotFocus += new RoutedEventHandler(RemoveText);
            keyInput.LostFocus += new RoutedEventHandler(AddText);
            passwordInput.GotFocus += new RoutedEventHandler(RemoveTextPassword);
            passwordInput.LostFocus += new RoutedEventHandler(AddTextPassword);
        }

        private async void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginValidation())
            {
                var auth = new Authentication();
                var pass = Md5Convert(passwordInput.Password);


                var result = await auth.AuthRequest(loginInput.Text, pass);
                if (result.result)  //Warning right - (result.Result)
                {
                    if (await GetKeyRequest()) //Warning right - (await GetKeyRequest())
                    {
                        loginPanel.Visibility = Visibility.Hidden;
                        keyPanel.Visibility = Visibility.Visible;
                        //System.IO.File.WriteAllLines("authInfo.txt", new string[] { loginInput.Text, sb.ToString() });
                        userEmail = loginInput.Text;
                        hashPass = pass;
                        sessionKey = result.sessionKey;
                        MessageBox.Show("Activation key was sent to your email", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Somethisng went wrong with authentication\nTry to login in several minutes", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Authetication filed", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
        }

        private async Task<bool> GetKeyRequest()
        {
            var req = new KeyRequest();
            var response = await req.Request(new KeyRequestModel { programHash = this.programHash, sessionKey = this.sessionKey });
            if (response.result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LoginValidation()
        {
            if(loginInput.Text == "")
            {
                MessageBox.Show("Enter login", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (passwordInput.Password == "")
            {
                MessageBox.Show("Enter password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private async void keySubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (keyInput.Text == "")
            {
                MessageBox.Show("Enter license key");
                return;
            }
            var activator = new ActivateKey(keyInput.Text);
            var response = await activator.Activate();
            if (!response.result)  //Warning right - (!response.Error)
            {
                MessageBox.Show("Problem with key activation:\n" + response.error, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var writer = new DeviceDataWriter();
                var model = await writer.WriteData(this.programHash, this.sessionKey);
                if (!model.result) //Warning right - (!model.result)
                {
                    MessageBox.Show("There was problem with activation\n" + model.error, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.IO.File.WriteAllLines("authInfo.txt", new string[] { userEmail, hashPass, licenseKey });
                    var mainWindow = new MainWindow(userEmail);
                    mainWindow.Show();
                    this.Close();
                }


                
            }
        }

        private void RemoveText(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box.Name == "loginInput")
            {
                if (box.Text == "Email")
                {
                    box.Text = "";
                }
            }
            else if (box.Name == "keyInput")
            {
                if (box.Text == "License key")
                {
                    box.Text = "";
                }
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box.Name == "loginInput")
            {
                if (String.IsNullOrWhiteSpace(box.Text))
                {
                    box.Text = "Email";
                }
            }
            else if (box.Name == "keyInput")
            {
                if (String.IsNullOrWhiteSpace(box.Text))
                {
                    box.Text = "License key";
                }
            }
            
        }

        private void RemoveTextPassword(object sender, EventArgs e)
        {
            var box = sender as PasswordBox;
            if (box.Name == "passwordInput")
            {
                if (box.Password == "Password")
                {
                    box.Password = "";
                }
            }
        }

        private void AddTextPassword(object sender, EventArgs e)
        {
            var box = sender as PasswordBox;
            if (box.Name == "passwordInput")
            {
                if (String.IsNullOrWhiteSpace(box.Password))
                {
                    box.Password = "Password";
                }
            }

        }

        private string Md5Convert(string text)
        {
            var res = "";

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(text);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var item in hashBytes)
            {
                sb.Append(item.ToString("X2"));
            }
            res = sb.ToString();

            return res;
        }


    }
}
