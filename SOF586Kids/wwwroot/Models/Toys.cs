using System;
using System.Collections.Generic;

namespace SOFKids.wwwroot.Models
{
    public partial class Toys
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
