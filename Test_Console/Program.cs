using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifoGroup;

namespace Test_Console
{
    internal class Program
    {
        class person
        {
            public string Name { get; set; }
        }
        static void Main(string[] args)
        {
            person mperson = null;
            Console.WriteLine(nameof(mperson));
            Console.ReadKey();
        }
    }
}
