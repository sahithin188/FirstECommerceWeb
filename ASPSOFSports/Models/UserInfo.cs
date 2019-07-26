using System;
using System.Collections.Generic;

namespace ASPSOFSports.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            PaymentAddress = new HashSet<PaymentAddress>();
            PurchaseHistory = new HashSet<PurchaseHistory>();
        }

        public int UserInfoId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<PaymentAddress> PaymentAddress { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
