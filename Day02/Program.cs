using System;
using System.Diagnostics;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            a();
            b();
        }
        
        static void a()
        {
            int x = 0;
            int y = 0;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                var parts = line.Split(' ');

                var action = parts[0];
                var value = Int32.Parse(parts[1]);
                
                switch (action)
                {
                    case "forward":
                        x += value;
                        break;
                    
                    case "down":
                        y += value;
                        break;
                    case "up":
                        y -= value;
                        break;
                }
            }
            
            Console.WriteLine(x*y);
        }
        
        static void b()
        {
            int x = 0;
            int y = 0;
            int aim = 0;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                var parts = line.Split(' ');
                var action = parts[0];
                var value = Int32.Parse(parts[1]);
                
                switch (action)
                {
                    case "forward":
                        x += value;
                        y += (aim * value);
                        break;
                    
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }
            
            Console.WriteLine(x*y);
        }
        
    }
}