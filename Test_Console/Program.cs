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
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.AddRange(new string[] {"11","22"});
            test(list);
            foreach(string a in list)
            {
                Console.WriteLine(a);
            }
            Console.ReadKey();
        }
        static public void test(List<string> a)
        {
            a.Clear();
            a.AddRange(new string[] { "1", "2", "3" });
        }
    }
}
