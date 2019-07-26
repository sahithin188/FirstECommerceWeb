using System;
using System.Collections.Generic;

namespace ASPSOFSports.Models
{
    public partial class Items
    {
        public Items()
        {
            PurchaseHistory = new HashSet<PurchaseHistory>();
        }

        public int ItemsId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
