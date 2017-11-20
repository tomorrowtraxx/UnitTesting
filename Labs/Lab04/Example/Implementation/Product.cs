using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation
{
    public struct Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(string name, double price)
            :this()
        {
            Name = name;
            Price = price;
        }
    }
}
