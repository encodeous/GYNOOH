using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GYNOOHLIB;
using GYNOOHLIB.Encryption.Random;

namespace RandomTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomNumber rn = new RandomNumber();

            for (int i = 0; i<100; i++)
            {
                
                var a = rn.Next(9999,10);
                Console.WriteLine(a);
            }
            Console.ReadLine();
        }
    }
}
