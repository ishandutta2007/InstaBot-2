using AndroidManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestADBManagement.BotTasks.Models;

namespace TestADBManagement.BotTasks
{
    public class FollowersWrapper
    {
        private InstagramDataContext db = new InstagramDataContext();
        private bool firstUse;

        public FollowersWrapper(bool fUse = false)
        {
            firstUse = fUse;
        }

        public void StartWrapping(string[] namesArray, int count)
        {
            WrapFollowers(namesArray, count);
        }

        private void WrapFollowers(string[] namesArray, int count)
        {
            var accounts = db.InstagramAccounts;
            var rnd = new Random();
            int index = rnd.Next(accounts.Count());
            foreach (var item in accounts.OrderBy(m => m.Id).Skip(index).Take(count))
            {
                AddFollower(namesArray, item);
                Thread.Sleep(15000);
            }
        }

        private void AddFollower(string[] namesArray, InstagramAccount account)
        {
            var instagram = new InstagramControl();
            instagram.RunInstagram();
            instagram.Login(account.AccountName, account.InstagramPass, firstUse);
            Thread.Sleep(1000);
            foreach (var name in namesArray)
            {
                instagram.UserNavigate(name);
                Thread.Sleep(1000);
                instagram.FollowTap();
                Thread.Sleep(500);
            }
            instagram.Logout();
            //BrowserControl browser = new BrowserControl();
            //browser.LoginInstagram(account.AccountName, account.InstagramPass);
            //browser.OpenPage(name);
            //Thread.Sleep(2000);
            //browser.FollowTap();
            //Thread.Sleep(1000);
            //browser.ExitAccount();
        }
    }
}
