using SharpAdbClient;
using SharpAdbClient.DeviceCommands;
using TestADBManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestADBManagement
{
    public class ADBControl
    {
        public void StartADB()
        {
            
        }
        public string[] GetDevices()
        {
            var res = new string[10];
            var devices = AdbClient.Instance.GetDevices();
            int i = 0;
            foreach (var item in devices)
            {
                res[i] = item.Name;
                i++;
            }
            return res;
        }
        public void InstallApp(string path)
        {

            var device = CreateDevice();
            var manager = new PackageManager(device);
            manager.InstallPackage("D:/instagram.apk", reinstall: false);
            //not working

            //ProcessStartInfo lcmdInfo1;

            //lcmdInfo1 = new ProcessStartInfo(" adb.exe ", @"adb install D:/instagram.apk");
            //lcmdInfo1.CreateNoWindow = true;
            //lcmdInfo1.RedirectStandardOutput = true;
            //lcmdInfo1.RedirectStandardError = true;
            //lcmdInfo1.UseShellExecute = false;

            //Process cmd2 = new Process();
            //cmd2.StartInfo = lcmdInfo1;

            //var output = new StringBuilder();
            //var error = new StringBuilder();

            //cmd2.OutputDataReceived += (o, ef) => output.Append(ef.Data);
            //cmd2.ErrorDataReceived += (o, ef) => error.Append(ef.Data);
            //cmd2.Start();
            //cmd2.BeginOutputReadLine();
            //cmd2.BeginErrorReadLine();
            //cmd2.WaitForExit();
            //cmd2.Close();
            //cmd2.Dispose();
        }
        public ConsoleOutputReceiver RunInstagram()
        {
            //adb shell monkey -p com.instagram.android -c android.intent.category.LAUNCHER 1
            var receiver = ExecuteShellOperation("monkey -p com.instagram.android -c android.intent.category.LAUNCHER 1");

            return receiver;
        }
        public string SignInInstagram(string login, string password)
        {
            var receiver = LoginNavigation() + "\n";
            receiver += LoginTextBoxClick() + "\n";
            receiver += LoginInput(login);
            receiver += PasswordTextBoxClick();
            receiver += PasswordInput(password);
            receiver += LoginButtonClick();

            return receiver;
        }
        public string FollowButton1Click()
        {
            //1200, 153
            var receiver = ExecuteShellOperation("input tap 1200 153");

            return receiver.ToString();
        }
        public string FollowButton2Click()
        {
            //1200, 205
            var receiver = ExecuteShellOperation("input tap 1200 205");

            return receiver.ToString();
        }
        public string FollowButton3Click()
        {
            //1200, 255
            var receiver = ExecuteShellOperation("input tap 1200 255");

            return receiver.ToString();
        }
        public void HomeClick()
        {
            //125 700
            var receiver = ExecuteShellOperation("input tap 125 700");
        }
        public void SearchClick()
        {
            //380 700
            var receiver = ExecuteShellOperation("input tap 380 700");
        }
        public void PhotoClick()
        {
            //640 700
            var receiver = ExecuteShellOperation("input tap 640 700");
        }
        public void LikesClick()
        {
            //895 700
            var receiver = ExecuteShellOperation("input tap 895 700");
        }
        public void ProfileClick()
        {
            //1150 700
            var receiver = ExecuteShellOperation("input tap 1150 700");
        }
        public string BackButtonPress()
        {
            //adb shell input keyevent 4
            var receiver = ExecuteShellOperation("input keyevent 4");
            return receiver.ToString();
        }

        private ConsoleOutputReceiver LoginNavigation()
        {
            //650, 690
            var receiver = ExecuteShellOperation("input tap 650 690");

            return receiver;
        }
        private ConsoleOutputReceiver LoginTextBoxClick()
        {
            //50, 305
            var receiver = ExecuteShellOperation("input tap 50 305");

            return receiver;
        }
        private ConsoleOutputReceiver LoginInput(string login)
        {
            //adb shell input text 'this%sis%san%sexample'
            var receiver = ExecuteShellOperation(String.Format("input text '{0}'", login));

            return receiver;
        }
        private ConsoleOutputReceiver PasswordTextBoxClick()
        {
            //80, 370
            var receiver = ExecuteShellOperation("input tap 80 370");

            return receiver;
        }
        private ConsoleOutputReceiver PasswordInput(string password)
        {
            //adb shell input text 'this%sis%san%sexample'
            var receiver = ExecuteShellOperation(String.Format("input text '{0}'", password));

            return receiver;
        }
        private ConsoleOutputReceiver LoginButtonClick()
        {
            //640, 425
            var receiver = ExecuteShellOperation("input tap 640 425");

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
        private ConsoleOutputReceiver ExecuteShellOperation(string operation)
        {
            var device = CreateDevice();
            var receiver = CreateReceiver();

            AdbClient.Instance.ExecuteRemoteCommand(operation, device, receiver);

            return receiver;
        }
    }
}
