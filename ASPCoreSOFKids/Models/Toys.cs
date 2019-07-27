using System;
using System.Collections.Generic;

namespace ASPCoreSOFKids.Models
{
    public partial class Toys
    {
        public Toys()
        {
            CartToys = new HashSet<CartToys>();
            PurchaseHistory = new HashSet<PurchaseHistory>();
        }

        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CartToys> CartToys { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
