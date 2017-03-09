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
    public class LocationLiker
    {
        private string[] postUrls;
        public void StartLike(string link, int count, InstagramAccount account)
        {
            var instagram = new InstagramControl();

            instagram.Login(account.AccountName, account.InstagramPass, false);
            Thread.Sleep(2000);

            instagram.LocationPostOpen(link);

            postUrls = new string[count];
            for (int i = 0; i < count; i++)
            {
                var res = instagram.GetPostUrl();
                if (i != 0)
                {
                    if (postUrls[i - 1] != res)
                    {
                        postUrls[i] = res;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    postUrls[i] = res;
                }
            }

            instagram.ReturnHome();
            //instagram.ReturnHome();

            Thread.Sleep(2000);
            foreach (var item in postUrls) instagram.LikePost(item);

            instagram.Logout();

        }
    }
}
