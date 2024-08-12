using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW_03_07
{
    internal class AdditionalTask2
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Welcome!\nAfter you press the button there would be delay for some random amount of time" +
                "\nand after that delay there would be signal and then you must press any button as fast as possible" +
                "\nSo good luck!)\nPress any button to start game...");
            Console.ReadKey();
            Console.Clear();

            int delay = new Random().Next(3, 11) * 1000;
            Thread.Sleep(delay);

            Console.WriteLine("Now! Press any button!");

            stopwatch.Start();
            Console.ReadKey();
            stopwatch.Stop();

            Console.Clear();

            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
