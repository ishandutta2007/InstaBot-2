using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.BotTasks.Models
{
    public class InstagramAccount
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string InstagramPass { get; set; }
        public string Email { get; set; }
        public string EmailPass { get; set; }
        public override string ToString()
        {
            var res = AccountName;
            return res;
        }
    }
}
