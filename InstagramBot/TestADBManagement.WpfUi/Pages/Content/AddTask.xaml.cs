using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestADBManagement.WpfUi.Pages.TasksView;
using TestADBManagement.WpfUi.VM;

namespace TestADBManagement.WpfUi.Pages.Content
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Page
    {
        public IEnumerable<VM_Tasks> Tasks { get; } = new List<VM_Tasks> {
                new VM_Tasks {
                    TaskId = 0,
                    Title = "Wrap followers",
                    Subtitle = "texttext",
                    ImageLink = "/Sources/followers.png",
                    TaskView = new WrapFollowersView() },
                new VM_Tasks {
                    TaskId =1,
                    Title = "Wrap likes to post",
                    Subtitle = "texttext",
                    ImageLink ="/Sources/likes_wrap.png",
                    TaskView = "Content here gonna be displayed by ContentControl..." },
                new VM_Tasks {
                    TaskId =2,
                    Title = "Mass Likeing",
                    Subtitle = "texttext",
                    ImageLink = "/Sources/target-like.png",
                    TaskView = "Content here gonna be displayed by ContentControl..." },
                new VM_Tasks {
                    TaskId =3,
                    Title = "Mass Following",
                    Subtitle = "texttext",
                    ImageLink = "/Sources/target-follow.png",
                    TaskView = "Content here gonna be displayed by ContentControl..." },
                new VM_Tasks {
                    TaskId =4,
                    Title = "Unfollow",
                    Subtitle = "texttext",
                    ImageLink = "/Sources/unfollow-icon.png",
                    TaskView = "Content here gonna be displayed by ContentControl..." }
            };

        public AddTask()
        {
            InitializeComponent();
        }
    }
}
