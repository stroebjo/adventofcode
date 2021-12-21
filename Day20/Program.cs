using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            A();
            B();
        }

        private static char[] replacement;

        private static int height;
        private static int width;
        private static bool[,] field;

        static void A()
        {
            var lines = System.IO.File.ReadLines(@"input.txt").ToArray();
            replacement = lines[0].ToArray();
            LoadField(lines[2..]);

            for (int i = 0; i < 2; i++)
            {
                field = Enhance(field);
            }

            //PrintField();
            Console.WriteLine(CountLitPixels());
        }
        
        static void B()
        {
            var lines = System.IO.File.ReadLines(@"input.txt").ToArray();
            replacement = lines[0].ToArray();
            LoadField(lines[2..]);

            for (int i = 0; i < 50; i++)
            {
                field = Enhance(field);
            }

            //PrintField();
            Console.WriteLine(CountLitPixels());
        }

        private static bool[,] Enhance(bool[,] field)
        {
            bool[,] newField = new bool[field.GetLength(0), field.GetLength(1)];

            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    newField[y, x] = replacement[GetEnhancementIndex(x, y)] == '#';
                }
            }

            return newField;
        }

        private static int CountLitPixels()
        {
            int count = 0;
            
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    if (field[y, x])
                    {
                        count += 1;
                    }
                }
            }

            return count;
        }

        private static int GetEnhancementIndex(int x, int y)
        {
            int idx = 0;
            
            var xAxis = new int[] {-1, 0, 1};
            var yAxis = new int[] {-1, 0, 1};

            int i = 1;
            foreach (var yD in yAxis)
            {
                foreach (var xD in xAxis)
                {
                    
                    int bit = 0;
                    
                    var xTest = x + xD;
                    var yTest = y + yD;

                    if ((0 <= xTest && xTest < field.GetLength(1)) && (0 <= yTest && yTest < field.GetLength(0)))
                    {
                        if (field[y + yD, x + xD])
                        {
                            bit = 1;
                        }     
                    }
                    else
                    {
                        // surrounding padding depends on the 0, 512 indizes of the replacement string.
                        // in the edges we need to check that wie use the right padding
                        if (field[0, 0])
                        {
                            bit = 1;
                        }
                    }
                    
                    idx <<= 1;
                    idx |= bit;
                    
                    i += 1;
                }
            }
            return idx;
        }

        
        private static void LoadField(string[] lines)
        {
            int buffer = 200;
            height = lines.Length + buffer;
            width = lines[0].Length + buffer;

            field = new bool[height, width];
            
            // build field
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    field[y+buffer/2, x+buffer/2] = (lines[y][x] == '#');
                }
            }
        }
        
        static void PrintField()
        {
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    var c = (field[y, x]) ? '#' : '.';
                    Console.Write(c);
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}