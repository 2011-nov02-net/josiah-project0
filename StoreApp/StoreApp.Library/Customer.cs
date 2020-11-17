using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace StoreApp.Library
{
    [DataContract(Name = "Customer")]
    public class Customer
    {
        [DataMember]
        public string FirstName { get; }
        [DataMember]
        public string LastName { get; }

        public int ID { get; }

        /// <summary>
        /// constructor of customer that accepts ID, useful when multiple
        /// customers with the same name exist.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="l"></param>
        /// <param name="id"></param>
        public Customer(string f, string l, int id)
        {
            FirstName = f; LastName = l; ID = id;
        }
        /// <summary>
        /// constructor of customer that does not accept an ID.
        /// Mostly used by the console app side since customers
        /// are not asked for their ID number.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="l"></param>
        public Customer(string f, string l)
        {
            FirstName = f; LastName = l; ID = 0;
        }
        private Customer() { }
        public bool Equals(Customer c)
        {
            if (FirstName == c.FirstName && LastName == c.LastName) return true;
            return false;
        }
        /// <summary>
        /// overridden Equals method so it can work with collection operations
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Customer);
        }
        public static bool operator ==(Customer c1, Customer c2)
        {
            if (c1.Equals(c2)) return true;
            return false;
        }
        public static bool operator !=(Customer c1, Customer c2){
            return (!(c1==c2));
        }
    }
}
