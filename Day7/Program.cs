using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var pos = ReadNumbers(@"input.txt");

            Console.WriteLine("brute force: {0}", A(pos));
            Console.WriteLine("mean: {0}", A2(pos));
            
            B(pos);
        }


        private static int[] ReadNumbers(string fileName)
        {
            string[] lines = System.IO.File.ReadLines(fileName).ToArray();
            return lines[0].Split(",").Select(int.Parse).ToArray();
        }
        
        static int A(int[] pos)
        {
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

            return best;
        }
        
        static int A2(int[] pos)
        {
            Array.Sort(pos);
            var median = pos[pos.Length / 2];

            var best = 0;
            for (var i = 0; i < pos.Length; i++)
            {
                best += Math.Abs(pos[i] - median);
            }

            return best;
        }

        static void B(int[] pos)
        {
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