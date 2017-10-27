/**
 * VK Person Logger
 * Author: Petr Osetrov (p.osetrov@yandex.ru)
 * Version: 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKPersonLogger
{

    /// <summary>
    /// Common class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Point of the enter in the program.
        /// </summary>
        static void Main()
        {
            // Test sample for using program.
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            // Get token via special method (follow to instructions).
            string token = Utilities.GetToken();
            var person = new VKPerson(Utilities.GetInt("VK ID"), token);

            // Start collecting...
            person.Collector.Start();

            Console.ReadKey();
            person.Collector.Stop();
        }
    }
}
