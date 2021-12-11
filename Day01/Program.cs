using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            int last = 0;

            List<int> all = new List<int>();

            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                all.Add(Int32.Parse(line));
            }

            for (int i = 0; i < all.Count - 2; i++ )
            {
                int current = 0;
                for (int w = 0; w < 3; w++)
                {
                    current += all[i + w];
                    Console.WriteLine(all[i + w]);
                }
                
                Console.WriteLine(current);
                Console.WriteLine();

                if (last == 0)
                {
                    last = current;
                    continue;
                }

                if (current > last)
                {
                    counter += 1;
                }
                
                last = current;

            }

            Console.WriteLine("Increases {0}", counter);
        }

        static void b()
        {
            
        }

        static void a()
        {   
            int counter = 0;
            int last = 0;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                var depth = Int32.Parse(line);

                if (last == 0)
                {
                    last = depth;
                    continue;
                }

                if (depth > last)
                {
                    counter++;  
                }

                last = depth;
            }  
            
            Console.WriteLine("Increases {0}", counter);
        }
    }
}