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
    public class LikesWrapper
    {
        private InstagramDataContext db = new InstagramDataContext();

        public LikesWrapper()
        {
            
        }

        public void StartWrapping(string[] posts, int count)
        {
            WrapLikes(posts, count);
        }



        private void WrapLikes(string[] posts, int count)
        {
            var accounts = db.InstagramAccounts;
            var rnd = new Random();
            int index = rnd.Next(accounts.Count()-count);
            foreach (var item in accounts.OrderBy(m=>m.Id).Skip(index).Take(count))
            {
                LikePosts(posts, item);
                Thread.Sleep(15000);
            }
        }

        private void LikePosts(string[] posts, InstagramAccount account)
        {
            var insta = new InstagramControl();
            insta.RunInstagram();
            insta.Login(account.AccountName, account.InstagramPass, false);
            Thread.Sleep(1000);
            foreach(var post in posts)
            {
                insta.LikePost(post);
            }
            insta.Logout();
        }
    }
}
