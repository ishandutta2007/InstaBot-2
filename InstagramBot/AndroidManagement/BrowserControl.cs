using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManagement
{
    public class BrowserControl : AndroidControl
    {
        public void LoginInstagram(string username, string password)
        {
            OpenInstagram();
            ClearInput(540, 570);
            TextInput(username);
            ClearInput(540, 620);
            TextInput(password);
            ScreenTap(665, 665);
        }

        public void ExitAccount()
        {
            ScreenTap(1145, 160);
            ScreenTap(958, 258);
            ScreenTap(665, 480);
        }

        public void FollowTap()
        {
            ScreenTap(858, 260);
        }

        public void WebPageNavigate(string uri)
        {
            ShellOperationExecute(string.Format("am start -a android.intent.action.VIEW -d {0}", uri));
        }

        public void OpenPage(string uri)
        {
            ScreenTap(645, 105);
            KeyInput(67);
            TextInput(uri);
            KeyInput(66);
        }



        private void OpenInstagram()
        {
            WebPageNavigate("https://www.instagram.com/");
        }

        
    }
}
