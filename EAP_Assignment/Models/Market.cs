using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAP_Assignment.Models
{
    public class Market
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Coin> Coins { get; set; }

        public Market()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = 1;
        }
    }
}