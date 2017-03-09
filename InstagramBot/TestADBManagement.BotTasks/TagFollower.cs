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
    public class TagFollower
    {
        public void StartFollow(string tag, int count, InstagramAccount account)
        { 
            var instagram = new InstagramControl();
            instagram.Login(account.AccountName, account.InstagramPass, false);
            Thread.Sleep(1000);
            instagram.TagFollowing(tag, count);
            Thread.Sleep(1000);
            instagram.Logout();
        }

        
    }
}
