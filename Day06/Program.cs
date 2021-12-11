using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            A();
            B();
        }

        static void A()
        {
            Console.WriteLine("After 80 days: {0}", LaternfishPop(80));
        }

        static void B()
        {
            Console.WriteLine("After 256 days: {0}", LaternfishPop(256));
        }

        private static long LaternfishPop(int runDays)
        {
            string[] lines = System.IO.File.ReadLines(@"input.txt").ToArray();
            int[] start = lines[0].Split(",").Select(int.Parse).ToArray();
            
            // get start
            long[] lifeCycle = new long[9];
            for (int l = 0; l < start.Length; l++)
            {
                lifeCycle[start[l]] += 1;
            }

            for (int days = 0; days < runDays; days++)
            {
     
                // how many new
                var spawn = lifeCycle[0];

                var nextLifeCycle = new long[lifeCycle.Length];
                Array.Copy(lifeCycle, 1, nextLifeCycle, 0, lifeCycle.Length - 1);
                
                nextLifeCycle[6] += spawn; // parents
                nextLifeCycle[8] += spawn;
                
                lifeCycle = nextLifeCycle;
            }

            return lifeCycle.Sum();
        }
    }
}