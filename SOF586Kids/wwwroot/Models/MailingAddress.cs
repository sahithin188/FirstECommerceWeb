using System;
using System.Collections.Generic;

namespace SOFKids.wwwroot.Models
{
    public partial class MailingAddress
    {
        public int MailingAddressId { get; set; }
        public int? CoustmerId { get; set; }
        public string Street { get; set; }
        public string AptNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public virtual CoustmerDetails Coustmer { get; set; }
    }
}
