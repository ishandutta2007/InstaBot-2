using AndroidManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestADBManagement.BotTasks;
using TestADBManagement.BotTasks.Models;
using TestADBManagement.HardwareInfoReceiver;

namespace TestADBManagement
{
    public partial class Form1 : Form
    {
        InstagramDataContext db = new InstagramDataContext();
        List<InstagramAccount> accounts = new List<InstagramAccount>();
        int currentAccount = -1;
        InstagramControl instagram = new InstagramControl();
        public Form1()
        {
            InitializeComponent();
            accounts = db.InstagramAccounts.ToList();
            var firstAcc = accounts.First();
            if (firstAcc != null)
            {
                outputLogin.Text = firstAcc.AccountName;
                outputPassword.Text = firstAcc.InstagramPass;
                currentAccount = accounts.FindIndex(m=>m == firstAcc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            foreach (var item in host.GetDevices())
            {
                //output.Text += item + "\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.InstallApp(@"D:\instagram.apk\");
            //output.Text += "App has been installed\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            instagram.RunInstagram();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool firstRun = checkBox1.Checked;
            instagram.Login("test_account_brooler", "TestPassword", firstRun);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.FollowButton2Click();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.FollowButton1Click();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.FollowButton3Click();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            var response = host.BackButtonPress();
            //output.Text += response + "\n";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.HomeClick();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.SearchClick();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.PhotoClick();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.LikesClick();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var host = new ADBControl();
            host.ProfileClick();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var data = new DataClass();
            data.CreateAccountsList();
            foreach (var item in data.Accounts)
            {
                //output.Text += item + "\n";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var browser = new BrowserControl();
            browser.LoginInstagram("test_account_brooler", "TestPassword");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //BrowserControl browser = new BrowserControl();
            //browser.ExitAccount();
            var instagram = new InstagramControl();
            instagram.ScreenTap(500, 500);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FollowersWrapper wrapper;
            if (checkBox1.Checked)
            {
                wrapper = new FollowersWrapper(true);
            }
            else
            {
                wrapper = new FollowersWrapper(false);
            }
            var namesArray = profileInput.Text.Split(',');
            //wrapper.StartWrapping(namesArray);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var postArray = postInput.Text.Split(',');
            var wrapper = new LikesWrapper();
            //wrapper.StartWrapping(postArray);
        }

        private void ButtonOne_Click(object sender, EventArgs e)
        {
            var location = locationInput.Text;
            var liker = new LocationFollower();

            var account = new InstagramAccount
            {
                AccountName = loginInput.Text,
                InstagramPass = passwordInput.Text
            };

            //liker.StartLocationFollow(location, account);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var db = new InstagramDataContext();
            var accounts = db.InstagramAccounts;
            var account = accounts.Find(1);
            MessageBox.Show(account.ToString());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var follower = new TagFollower();
            //follower.StartFollow(tagInput.Text, new InstagramAccount { AccountName = loginInput.Text, InstagramPass = passwordInput.Text });
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var liker = new TagLiker();
            liker.StartLike(likeTagInput.Text, int.Parse(likeCountInput.Text), new InstagramAccount { AccountName = loginInput.Text, InstagramPass = passwordInput.Text });

        }

        private void button22_Click(object sender, EventArgs e)
        {
            var liker = new LocationLiker();
            liker.StartLike(likeLocationUrlInput.Text, int.Parse(locationLikeCountInput.Text), new InstagramAccount { AccountName = loginInput.Text, InstagramPass = passwordInput.Text });
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var info = new BiosInfo();
            MessageBox.Show(info.Manufacturer + "\n" + info.IdentificationCode + "\n" +
                info.SerialNumber + "\n" + info.ReleaseDate + "\n" + info.Version);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            var info = new MotherboardInfo();
            MessageBox.Show(info.Manufacturer + "\n" + info.Product + "\n" + info.SerialNumber);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var info = new DiskInfo();
            var result = "";
            foreach (var item in info.SerialNumber)
            {
                result += item + "\n";
            }

            foreach(var item in info.Signature)
            {
                result += item + "\n";
            }

            foreach (var item in info.TotalSectors)
            {
                result += item + "\n";
            }
            MessageBox.Show(result);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            var info = new NetworkInfo();
            MessageBox.Show(info.MacAddress);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (currentAccount != -1)
            {
                try
                {
                    var account = accounts[currentAccount - 1];
                    currentAccount -= 1;
                    outputLogin.Text = account.AccountName;
                    outputPassword.Text = account.InstagramPass;
                }
                catch (Exception)
                {

                }
                
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                var account = accounts[currentAccount + 1];
                currentAccount += 1;
                outputLogin.Text = account.AccountName;
                outputPassword.Text = account.InstagramPass;
            }
            catch (Exception)
            {

            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            instagram.Login(outputLogin.Text, outputPassword.Text, false);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            instagram.Logout();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            var account = accounts[currentAccount];
            db.InstagramAccounts.Remove(account);
            db.SaveChanges();
        }
    }
}
