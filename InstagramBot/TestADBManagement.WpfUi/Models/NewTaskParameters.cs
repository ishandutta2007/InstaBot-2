using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.WpfUi.Models
{
    public abstract class NewTaskParameters 
    {
    }

    public sealed class WrapFollowersTaskParameters : NewTaskParameters
    {
        public string LinkToAccount { get; set; }
        public double MinInterval { get; set; }
        public double MaxInterval { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
