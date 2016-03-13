using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pizzas
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"pizzas.json");
            var orders = JsonConvert.DeserializeObject<List<Order>>(text);
            Console.WriteLine("pizzas count: {0}",orders.Count);

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

            Console.WriteLine("without duplicates: {0}", dict.Count);

            var top20 = (from d in dict
                         orderby d.Value descending
                         select d).Take(20).ToDictionary(k => k.Key, v => v.Value);
            foreach (var d in top20)
            {
                Console.WriteLine("times: {0}, toppings: {1}", d.Value, string.Join(",", d.Key.Toppings));
            }
            Console.ReadLine();
        }
    }
}
