using System;
using System.Collections.Generic;

namespace SOFKids.wwwroot.Models
{
    public partial class PurchaseHistory
    {
        public int PurchaseHistoryId { get; set; }
        public int? ToyId { get; set; }
        public int? CoustmerId { get; set; }
        public int? Quantity { get; set; }

        public virtual CoustmerDetails Coustmer { get; set; }
    }
}
