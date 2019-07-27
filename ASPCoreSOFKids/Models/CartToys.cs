using System;
using System.Collections.Generic;

namespace ASPCoreSOFKids.Models
{
    public partial class CartToys
    {
        public int CartToyId { get; set; }
        public int? ToyId { get; set; }
        public int? CoustmerId { get; set; }
        public int? Quantity { get; set; }

        public virtual CoustmerDetails Coustmer { get; set; }
    }
}
