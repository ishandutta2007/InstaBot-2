using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidManagement
{
    public class InstagramControl : AndroidControl
    {
        public void RunInstagram()
        {
            RunApplication("com.instagram.android");
        }

        public void StopInstagram()
        {
            StopApplication("com.instagram.android");
        }
        
        public void Login(string username, string password, bool firstRun)
        {
            LoginNavigation();
            Thread.Sleep(400);
            LoginClear();
            LoginInput(username);
            PasswordInput(password);
            TapLoginButton();
        }
        public void Logout()
        {
            ScreenTap(1150, 700);
            Thread.Sleep(1000);
            ScreenTap(1250, 28);
            Thread.Sleep(1000);
            Swipe(658, 605, 658, 300, 90);
            Thread.Sleep(1000);
            ScreenTap(100, 635);
            Thread.Sleep(1000);
            ScreenTap(786, 393);
        }
        //681, 89
        public void FollowTap()
        {
            ScreenTap(660, 157);
        }

        public void LikePost(string link)
        {
            for (int i=400; i<=726; i += 40)
            {
                TryLike(link, 26, i);
            }
            //RunInstagram();
        }

        public void UserNavigate(string name)
        {
            ScreenTap(380, 700);
            Thread.Sleep(1000);
            ScreenTap(50, 40);
            TextInput(name);
            EnterKeyInput();
            Thread.Sleep(1000);
            ScreenTap(51, 148);
        }

        public void GeoFollowing(string location, int count)
        {
            OpenUrl(location);
            Thread.Sleep(2000);
            Swipe(500, 500, 500, 190, 100);
            Thread.Sleep(1000);
            ScreenTap(50, 80);
            Thread.Sleep(1000);
            Swipe(500, 500, 500, 400, 300);
            for (int i = 0; i < count; i++) SmallFollowTap();

            //Thread.Sleep(2000);
            //ScreenTap(197, 472);
            //Thread.Sleep(1000);
            //Swipe(500, 500, 500, 300, 300);
            //for (int i = 0; i < 9; i++) SmallFollowTap();

            BackButtonTap();
            BackButtonTap();
        }

        public void TagFollowing(string tag, int count)
        {
            TagPostsFind(tag);
            for (int i = 0; i < count; i++) SmallFollowTap();
            BackButtonTap();
            BackButtonTap();
        }

        public void TagPostsFind(string tag)
        {
            OpenUrl(string.Format("https://www.instagram.com/explore/tags/{0}/", tag));
            Thread.Sleep(2000);
            Swipe(500, 500, 500, 190, 100);
            Thread.Sleep(1000);
            ScreenTap(50, 100);
            Thread.Sleep(1000);
            Swipe(500, 500, 500, 400, 300);
        }

        public string GetPostUrl()
        {
            ScreenTap(1250, 28);
            ScreenTap(500, 395);
            Thread.Sleep(1500);
            Swipe(500, 500, 500, 205, 100);
            var res = Clipboard.GetText();
            Thread.Sleep(500);
            return res;
        }

        public void ReturnHome()
        {
            BackButtonTap();
        }

        public void LocationPostOpen(string url)
        {
            OpenUrl(url);
            Thread.Sleep(2000);
            Swipe(500, 500, 500, 200, 100);
            Thread.Sleep(2000);
            ScreenTap(150, 100);
            Thread.Sleep(1000);
            Swipe(500, 500, 500, 400, 300);
        }

        public void Unfollow()
        {
            ScreenTap(686, 158);
            Thread.Sleep(300);
            ScreenTap(796, 471);
        }


        
        private void LoginNavigation()
        {
            //653, 732
            ScreenTap(235, 710);
        }

        private void LoginClear()
        {
            //481, 340
            ClearInput(40, 330);
        }
        private void LoginInput(string login)
        {
            TextInput(login);
        }
        private void PasswordInput(string pass)
        {
            //481, 374
            ScreenTap(467, 363);
            TextInput(pass);
        }
        private void TapLoginButton()
        {
            //660, 412
            ScreenTap(800, 432);
        }

        private void TryLike(string link, int x, int y)
        {
            OpenUrl(link);
            Thread.Sleep(500);
            Swipe(300, 700, 300, 300, 100);
            Thread.Sleep(300);
            ScreenTap(x, y);
            BackButtonTap();
        }

        private void SmallFollowTap()
        {
            ScreenTap(1213, 50);
            Swipe(500, 500, 500, 200, 100);
            Thread.Sleep(700);
            
        }
    }
}
