using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pizzas
{
    public class Order:IEquatable<Order>
    {
        public HashSet<string> Toppings { get; set; }

        public bool Equals(Order other)
        {
            return Toppings.SetEquals(other.Toppings);
        }

        public override bool Equals(object obj)
        {
            var toppings = obj as HashSet<string>;
            var other = new Order(){Toppings = toppings};
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return GetOrderIndependentHashCode(Toppings);
        }

        private static int GetOrderIndependentHashCode<T>(IEnumerable<T> source)
        {
            var hash = 0;
            foreach (var element in source)
            {
                hash = hash ^ EqualityComparer<T>.Default.GetHashCode(element);
            }
            return hash;
        }
    }
}
