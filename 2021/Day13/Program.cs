using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 0;
            int height = 0;
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();
            List<Tuple<char, int>> instructions = new List<Tuple<char, int>>();

            bool parseInstructions = false;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                if (line.Equals(""))
                {
                    parseInstructions = true;
                    continue;
                } 
                
                if (!parseInstructions)
                {
                    var coors = line.Split(','); // x, y
                    var t = new Tuple<int, int>(int.Parse(coors[0]), int.Parse(coors[1]));
                    points.Add(t);
                    
                    if (t.Item1 > width)
                    {
                        width = t.Item1;
                    }               
                    
                    if (t.Item2 > height)
                    {
                        height = t.Item2; 
                    }
                }
                else
                {
                    var words = line.Split(' ');
                    var instr = words[2].Split('=');
                    instructions.Add(new Tuple<char, int>(instr[0][0], int.Parse(instr[1])));
                }
            }
            
            // build field
            int[,] field = new int[height+1, width+1];
            foreach (var p in points)
            {
                field[p.Item2, p.Item1] = 1;
            }
            
            // first instruction
            var count = 0;
            foreach (var instr in instructions)
            {
                if (instr.Item1 == 'y')
                {
                    // fold up
                    field = FoldY(field, instr.Item2);
                }
                else
                {
                    field = FoldX(field, instr.Item2);
                }

                if (count == 0)
                {
                    count = CountPoints(field);
                    Console.WriteLine("A) {0}", count);
                }
            }

            Console.WriteLine("B)");
            PrintField(field);
        }

        static int CountPoints(int[,] field)
        {
            int count = 0;
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    if (field[y, x] > 0)
                    {
                        count += 1;
                    }
                }
            }

            return count;
        }


        static int[,] FoldY(int[,] field, int atY)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);

            var newHeight = width / 2;

            int[,] fieldUpper = new int[newHeight, width];
            int[,] fieldLower = new int[newHeight, width];

            SplitY(field, atY, out fieldUpper, out fieldLower);
            fieldLower = MirrorY(fieldLower);
            
            for (int y = 0; y < fieldUpper.GetLength(0); y++)
            {
                for (int x = 0; x < fieldUpper.GetLength(1); x++)
                {
                    if (fieldLower[y, x] > 0)
                    {
                        fieldUpper[y, x] = 1;
                    }
                }
            }

            return fieldUpper;
        }

        static int[,] FoldX(int[,] field, int atX)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);

            var newWidth = width / 2;

            int[,] fieldLeft = new int[height, newWidth];
            int[,] fieldRight = new int[height, newWidth];

            SplitX(field, atX, out fieldLeft, out fieldRight);
            fieldRight = MirrorX(fieldRight);

            for (int y = 0; y < fieldLeft.GetLength(0); y++)
            {
                for (int x = 0; x < fieldLeft.GetLength(1); x++)
                {
                    if (fieldRight[y, x] > 0)
                    {
                        fieldLeft[y, x] = 1;
                    }
                }
            }

            return fieldLeft;
        }
        
        static void SplitY(int[,] field, int ySplit, out int[,] fieldUpper, out int[,] fieldLower)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);
            var newHeight = height / 2;
            
            fieldUpper = new int[newHeight, width];
            fieldLower = new int[newHeight, width];
            
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    fieldUpper[y, x] = field[y, x];
                    
                    // for fold along y=447 this fails, since we have 
                    // even number of rows, so "just check" we don't leave our field. 
                    var lowerY = y + 1 + newHeight;
                    if (lowerY < height)
                    {
                        fieldLower[y, x] = field[y + 1 + newHeight, x];
                    }
                }
            }
        }
        
        static void SplitX(int[,] field, int xSplit, out int[,] fieldLeft, out int[,] fieldRight)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);
            var newWidth = width / 2;
            
            fieldLeft = new int[height, newWidth];
            fieldRight = new int[height, newWidth];
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < xSplit; x++)
                {
                    fieldLeft[y, x] = field[y, x];
                    fieldRight[y, x] = field[y, x+1+xSplit];
                }
            }
        }

        static int[,] MirrorY(int[,] field)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);
            int[,] newField = new int[height, width];
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newField[y, x] = field[height - 1 - y, x];
                }
            }

            return newField;
        }
        
        static int[,] MirrorX(int[,] field)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);
            int[,] newField = new int[height, width];
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newField[y, x] = field[y, width - 1 - x];
                }
            }

            return newField;
        }

        static void PrintField(int[,] field)
        {
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    if (field[y, x] > 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(field[y, x]);
                    
                    if (field[y, x] > 0 )
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