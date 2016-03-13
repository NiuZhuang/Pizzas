using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Pizzas
{
    public class Tests
    {
        [Test]
        public void Test_ReadJsonFile()
        {
            var text = File.ReadAllText(@"pizzas.json");
            var orders = JsonConvert.DeserializeObject<List<Order>>(text);
            Console.Write(orders.Count);
        }

        [Test]
        public void Test_Top20Config()
        {

            var text = File.ReadAllText(@"pizzas.json");
            var orders = JsonConvert.DeserializeObject<List<Order>>(text);
            Console.WriteLine(orders.Count);

            var dict = new Dictionary<Order, int>();

            foreach (var order in orders)
            {
                if (dict.ContainsKey(order))
                {
                    dict[order]++;
                }
                else
                {
                    dict.Add(order, 1);
                }
            }

            Console.WriteLine(dict.Count);


            var top20 = (from d in dict
                         orderby d.Value descending
                         select d).Take(20).ToDictionary(k => k.Key, v => v.Value);
            foreach (var d in top20)
            {
                Console.WriteLine("value: {1}, key: {0}", string.Join(",", d.Key.Toppings), d.Value);
            }

        }

        [Test]
        public void Test_EqualOrder()
        {
            var order1= new Order(){Toppings = new HashSet<string>(){"test1", "abc"}};
            var order2 = new Order() { Toppings = new HashSet<string>() { "abc", "test1" } };

            Assert.IsTrue(order1.Equals(order2));
        }

        [Test]
        public void Test_GetHashCode()
        {
            var order1 = new Order() { Toppings = new HashSet<string>() { "test1", "abc" } };
            var order2 = new Order() { Toppings = new HashSet<string>() { "abc", "test1" } };
            var order3 = new Order() { Toppings = new HashSet<string>() { "abcdef", "test1" } };

            Console.WriteLine(order1.GetHashCode());
            Console.WriteLine(order2.GetHashCode());
            Console.WriteLine(order3.GetHashCode());

            Assert.AreEqual(order1.GetHashCode(), order2.GetHashCode());
            Assert.AreNotEqual(order1.GetHashCode(), order3.GetHashCode());
        }
    }
}
