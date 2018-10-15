using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    class Program
    {
        private static Task t;
        static void Main(string[] args)
        {
            int i = 2;
            test();
            t.Start();
            Console.ReadLine();
        }
        private static async void test()
        {
            t = new Task(async () => { await Task.Delay(1000); });
            await t;
            Console.Write("tet");
        }
    }
}
