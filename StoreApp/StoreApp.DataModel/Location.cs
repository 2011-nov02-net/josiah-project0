using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataModel
{
    public partial class Location
    {
        public Location()
        {
            LocationLines = new HashSet<LocationLine>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LocationLine> LocationLines { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
