using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataModel
{
    public partial class Product
    {
        public Product()
        {
            LocationLines = new HashSet<LocationLine>();
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<LocationLine> LocationLines { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
