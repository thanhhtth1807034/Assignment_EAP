using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EAP_Assignment.Models
{
    public class MyDbContext : IdentityDbContext<Account>
    {
        public MyDbContext() : base("name=EAP_AssignmentContext")
        {

        }
        public static MyDbContext Create()
        {
            return new MyDbContext();
        }
        public System.Data.Entity.DbSet<EAP_Assignment.Models.Market> Markets { get; set; }
        public System.Data.Entity.DbSet<EAP_Assignment.Models.Coin> Coins { get; set; }
    }
}