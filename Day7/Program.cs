using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
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
            string[] lines = System.IO.File.ReadLines(@"input.txt").ToArray();
            int[] pos = lines[0].Split(",").Select(int.Parse).ToArray();

            var min = pos.Min();
            var max = pos.Max();

            var best = int.MaxValue;
            var current = 0;
            
            for (int c = 0; c < max; c++)
            {
                for (var i = 0; i < pos.Length; i++)
                {
                    current += Math.Abs(c - pos[i]);
                }

                if (current < best)
                {
                    best = current;
                }

                current = 0;
            }

            Console.WriteLine(best);
        }
        
        static void A2()
        {
            string[] lines = System.IO.File.ReadLines(@"input.txt").ToArray();
            int[] pos = lines[0].Split(",").Select(int.Parse).ToArray();
            
            Array.Sort(pos);
            var median = pos[pos.Length / 2];

            var best = 0;
            
            for (var i = 0; i < pos.Length; i++)
            {
                best += Math.Abs(pos[i] - median);
            }
                
            Console.WriteLine(best);
        }

        static void B()
        {
            string[] lines = System.IO.File.ReadLines(@"input.txt").ToArray();
            int[] pos = lines[0].Split(",").Select(int.Parse).ToArray();

            var max = pos.Max();

            var best = int.MaxValue;
            var current = 0;
            
            for (int c = 0; c < max; c++)
            {
                for (var i = 0; i < pos.Length; i++)
                {
                    var n = Math.Abs(c - pos[i]);
                    current += (n * (n + 1)) / 2;
                }

                if (current < best)
                {
                    best = current;
                }

                current = 0;
            }

            Console.WriteLine(best);
        }
    }
}