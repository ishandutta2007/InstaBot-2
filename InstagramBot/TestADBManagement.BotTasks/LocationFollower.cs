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
    public class LocationFollower
    {

        public LocationFollower()
        {

        }

        public void StartLocationFollow(string link, int count, InstagramAccount account)
        {
            OpenLocation(link, count, account);
        }

        private void OpenLocation(string link, int count, InstagramAccount account)
        {
            var instagram = new InstagramControl();
            instagram.Login(account.AccountName, account.InstagramPass, false);
            Thread.Sleep(1000);
            instagram.GeoFollowing(link, count);
            instagram.Logout();

        }

        private void OpenPost()
        {

        }
    }
}
