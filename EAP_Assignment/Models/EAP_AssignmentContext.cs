using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EAP_Assignment.Models
{
    public class EAP_AssignmentContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EAP_AssignmentContext() : base("name=EAP_AssignmentContext")
        {
        }

        public System.Data.Entity.DbSet<EAP_Assignment.Models.Market> Markets { get; set; }

        public System.Data.Entity.DbSet<EAP_Assignment.Models.Coin> Coins { get; set; }
    }
}
