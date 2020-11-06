using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    interface ILocation
    {
        bool checkAvail(Product p, int count);
    }
}
