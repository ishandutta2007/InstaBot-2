using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.BotTasks.Models
{
    public class InstagramDataContext : DbContext
    {
        public DbSet<InstagramAccount> InstagramAccounts { get; set; }

        public InstagramDataContext() : base("InstagramDataContext")
        {
        }
    }

    
}
