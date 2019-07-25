using System;
using System.Collections.Generic;

namespace SOFSports.wwwroot.Models
{
    public partial class PaymentAddress
    {
        public int PaymentAddressId { get; set; }
        public int? UserInfoId { get; set; }
        public string Street { get; set; }
        public string AptNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
