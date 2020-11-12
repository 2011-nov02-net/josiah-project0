using System.ComponentModel;
using System.Runtime.Serialization;

namespace StoreApp.Library
{
    [DataContract(Name = "Product")]
    public class Product
    {
        private int _amount;
        [DataMember]
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private double _discount;
        [DataMember]
        public double Discount { get { return _discount; } private set { _discount = value; } }

        [DataMember]
        public string Name { get; private set; }

        private double _price;
        [DataMember]
        public double Price {
            get { return _price * Discount; }
            private set { _price = value; }
        }

        public Product(string name, double price, int amount)
        {
            Name = name; Price = price; Amount = amount; Discount = 1;
        }
        private Product(string name, double price, int amount, double discount)
        {
            Name = name; Price = price; Amount = amount; Discount = discount;
        }
        private Product() { }
        public override bool Equals(object obj)
        {
            return (((Product)obj).Name == this.Name);
        }
        public static bool operator ==(Product c1, Product c2)
        {
            if (c1.Equals(c2)) return true;
            return false;
        }
        public static bool operator !=(Product c1, Product c2)
        {
            return (!(c1 == c2));
        }
        public void AddDiscount(double d)
        {
            _discount = d;
        }
        public void RemoveDiscount()
        {
            _discount = 1;
        }
    }
}
