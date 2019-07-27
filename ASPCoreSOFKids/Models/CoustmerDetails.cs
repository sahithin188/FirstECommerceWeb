using System;
using System.Collections.Generic;

namespace ASPCoreSOFKids.Models
{
    public partial class CoustmerDetails
    {
        public CoustmerDetails()
        {
            CartToys = new HashSet<CartToys>();
            MailingAddress = new HashSet<MailingAddress>();
            PurchaseHistory = new HashSet<PurchaseHistory>();
        }

        public int CoustmerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CartToys> CartToys { get; set; }
        public virtual ICollection<MailingAddress> MailingAddress { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
