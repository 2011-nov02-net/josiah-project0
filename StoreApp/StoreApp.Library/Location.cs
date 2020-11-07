using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Location : ILocation
    {

        bool ILocation.checkAvail(Product p, int count)
        {
            throw new NotImplementedException();
        }


    }
}
