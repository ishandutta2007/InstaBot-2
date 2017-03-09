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
    public class Unfollow
    {
        

        public void StartTask(InstagramAccount account, string[] names)
        {
            var instagram = new InstagramControl();
            instagram.RunInstagram();
            Thread.Sleep(1000);

            instagram.Login(account.AccountName, account.InstagramPass, false);
            Thread.Sleep(1000);
            foreach (string user in names)
            {
                instagram.UserNavigate(user);
                instagram.Unfollow();
                Thread.Sleep(1000);
            }
            instagram.Logout();
        }
    }
}
