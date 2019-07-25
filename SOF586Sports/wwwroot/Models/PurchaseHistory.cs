using System;
using System.Collections.Generic;

namespace SOFSports.wwwroot.Models
{
    public partial class PurchaseHistory
    {
        public int PurchaseHistoryId { get; set; }
        public int? ItemsId { get; set; }
        public int? UserInfoId { get; set; }
        public int? Quantity { get; set; }
        public bool IsPurchased { get; set; }

        public virtual Items Items { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
