using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManagement
{
    public class AndroidControl
    {
        public void BackButtonTap()
        {
            KeyInput(4);
        }

        public void EnterKeyInput()
        {
            KeyInput(66);
        }



        protected void RunApplication(string packageName)
        {
            //com.instagram.android
            ShellOperationExecute(string.Format("monkey -p {0} -c android.intent.category.LAUNCHER 1", packageName));
        }

        public void ScreenTap(int x, int y)
        {
            ShellOperationExecute(string.Format("input tap {0} {1}", x, y));
        }

        protected void TextInput(string text)
        {
            ShellOperationExecute(string.Format("input text '{0}'", text));
        }

        protected void ClearInput(int x, int y)
        {
            ScreenPress(x, y, 1000);
            KeyInput(67);
        }

        protected void ScreenPress(int x, int y, int duration)
        {
            Swipe(x, y, x, y, duration);
        }

        protected void Swipe(int x1, int y1, int x2, int y2, int duration)
        {
            ShellOperationExecute(string.Format("input swipe {0} {1} {2} {3} {4}", x1, y1, x2, y2, duration));
        }

        protected void KeyInput(int keycode)
        {
            ShellOperationExecute(string.Format("input keyevent {0}", keycode));
        }

        protected void FileTransfer(string path)
        {

        }

        public void OpenUrl(string url)
        {
            ShellOperationExecute(string.Format("am start {0}", url));
        }

        protected void StopApplication(string app)
        {
            ShellOperationExecute(string.Format("am force-stop {0}", app));
        }



        protected ConsoleOutputReceiver ShellOperationExecute(string operation)
        {
            var device = CreateDevice();
            var receiver = CreateReceiver();

            AdbClient.Instance.ExecuteRemoteCommand(operation, device, receiver);

            return receiver;
        }

        private DeviceData CreateDevice()
        {
            var device = AdbClient.Instance.GetDevices().First();
            return device;
        }

        private ConsoleOutputReceiver CreateReceiver()
        {
            var receiver = new ConsoleOutputReceiver();
            return receiver;
        }
    }
}
