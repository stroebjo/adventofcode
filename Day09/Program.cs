using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {

        static void Main(string[] args)
        {
            A();
            B();
        }

        private static bool[,] marked;
        private static int[,] field;
        
        static void A()
        {
            string[] lines = System.IO.File.ReadLines(@"input.txt").ToArray();

            int height = lines.Length;
            int width = lines[0].Length;

            field = new int[height, width];
            marked = new bool[height, width];
            
            // build field
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    field[y, x] = lines[y][x] - '0';
                }
            }

            var riskLevel = 0;
            var basinSizes = new List<int>(); 
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var z = field[y, x];
                    var isDepth = true;
                    
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

                            // no diagonals
                            if (xTest == x || yTest == y)
                            {
                                if ((0 <= xTest && xTest < width) && (0 <= yTest && yTest < height))
                                {
                                    if (field[yTest, xTest] <= z)
                                    {
                                        isDepth = false;
                                    }
                                }
                            }
                        }
                    }

                    if (isDepth)
                    {
                        riskLevel += z + 1;
                        
                        marked = new bool[height, width];
                        countBasin = 0;
                        GetBasinArea(x, y);
                        basinSizes.Add(countBasin);

                        //PrintBasin();
                    }
                }
            }
            
            Console.WriteLine($"A) Risk leve: {riskLevel}");
            
            basinSizes.Sort();
            //Console.WriteLine(String.Join(", ", basinSizes));
            Console.WriteLine($"B) Largest 3 basins: {basinSizes[^3] * basinSizes[^2] * basinSizes[^1]}");
        }

        private static int countBasin = 0;
        
        static void GetBasinArea(int x, int y)
        {
            var xAxis = new int[] {-1, 0, 1};
            var yAxis = new int[] {-1, 0, 1};
            
            foreach (var xD in xAxis)
            {
                foreach (var yD in yAxis)
                {
                    var xTest = x + xD;
                    var yTest = y + yD;

                    // no diagonals
                    if (xTest == x || yTest == y)
                    {
                        if ((0 <= xTest && xTest < field.GetLength(1)) && (0 <= yTest && yTest < field.GetLength(0)))
                        {
                            // is not yet marked
                            if (!marked[yTest, xTest])
                            {
                                if (field[yTest, xTest] < 9)
                                {
                                    marked[yTest, xTest] = true;
                                    countBasin += 1;
                                    //PrintBasin();

                                    GetBasinArea(xTest, yTest);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        static void PrintBasin()
        {
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    if (marked[y, x])
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(field[y, x]);
                    
                    if (marked[y, x])
                    {
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
        }
        
        static void B()
        {
        }
    }
}