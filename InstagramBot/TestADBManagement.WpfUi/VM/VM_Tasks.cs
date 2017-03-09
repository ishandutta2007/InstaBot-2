using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestADBManagement.WpfUi.VM
{
    public class VM_Tasks
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImageLink { get; set; }

        /// <summary>
        /// This property is a bidning source for XAML.
        /// All task views are in Pages/TasksView folder.
        /// </summary>
        public object TaskView { get; set; }
    }
}
