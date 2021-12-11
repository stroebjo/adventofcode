using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            A();
            B();
        }

        private static int[,] field;
        private static bool[,] flashed;

        private static int height;
        private static int width;

        private static int FlashCount = 0;
        
        static void A()
        {
            LoadField(@"input.txt");
            var steps = 100;

            for (int i = 0; i < steps; i++)
            {
                Tick();
            }
            
            Console.WriteLine($"Total flashes: {FlashCount}");
        }
        
        static void B()
        {
            LoadField(@"input.txt");

            var CountOctopuses = width * height;
            var TickCount = 0;
            
            while (CountOctopuses != FlashCount)
            {
                FlashCount = 0;
                Tick();
                TickCount += 1;
            }
            
            Console.WriteLine($"All flash in tick: {TickCount}");
        }

        private static void LoadField(string fileName)
        {
            string[] lines = System.IO.File.ReadLines(fileName).ToArray();

            height = lines.Length;
            width = lines[0].Length;

            field = new int[height, width];
            flashed = new bool[height, width];
            
            // build field
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    field[y, x] = lines[y][x] - '0';
                }
            }
        }

        private static void Tick()
        {
            flashed = new bool[height, width]; // reset all flashed to false
                
            // increase all by 1
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    field[y, x] += 1;
                }
            }
                
            // increase all adjacent to a flash until nothing flashes anymore
            var isFlashing = false;
            do
            {
                isFlashing = false;
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (field[y, x] > 9 && !flashed[y, x])
                        {
                            flashed[y, x] = true;
                            isFlashing = true;
                            IncreaseAdjacent(x, y);
                        }
                    }
                }
            } while (isFlashing);

                
            // set all flashed to 0 and count flashes
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (flashed[y, x])
                    {
                        FlashCount += 1;
                        field[y, x] = 0;
                    }
                }
            }
        }
        
        static void IncreaseAdjacent(int x, int y)
        {
            var xAxis = new int[] {-1, 0, 1};
            var yAxis = new int[] {-1, 0, 1};

            foreach (var xD in xAxis)
            {
                foreach (var yD in yAxis)
                {
                    var xTest = x + xD;
                    var yTest = y + yD;
                            
                    // exclude same point
                    if (xTest == x && yTest == y)
                    {
                        continue;
                    }
                    
                    if ((0 <= xTest && xTest < width) && (0 <= yTest && yTest < height))
                    {
                        // exclude same point
                        if (flashed[yTest, xTest])
                        {
                            continue;
                        }
                        
                        field[yTest, xTest] += 1;
                    }
                }
            }
        }

        static void PrintField()
        {
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    if (flashed[y, x])
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(field[y, x]);
                    
                    if (flashed[y, x])
                    {
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}