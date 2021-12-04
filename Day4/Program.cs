using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
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
            var lines = System.IO.File.ReadLines(@"input.txt").ToList();
            
            // first line for numbers
            var numbers = Array.ConvertAll(lines[0].Split(','), s => int.Parse(s));
            
            var field = new (bool, int)[5, 5];
            var fieldRowIndex = 0;

            var bestSteps = int.MaxValue;
            var answer = 0;

            for (var i = 2; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line == "")
                {
                    continue;
                }
 
                var a = System.Text.RegularExpressions.Regex.Split( line.Trim(), @"\s+");
                var row = Array.ConvertAll(a, s => int.Parse(s));

                for (int j = 0; j < row.Length; j++)
                {
                    field[fieldRowIndex, j] = (false, row[j]);
                }

                fieldRowIndex++;
                
                // finish bingo field
                if (fieldRowIndex == 5)
                {
                    var result = winningField(field, numbers);

                    if (result.Item1 < bestSteps)
                    {
                        bestSteps = result.Item1;
                        answer = result.Item2;
                    }
                    
                    fieldRowIndex = 0;
                }
            }
            
            Console.WriteLine("steps: {0}, answer: {1}", bestSteps, answer);
        }
        
        
        static void B()
        {
            var lines = System.IO.File.ReadLines(@"input.txt").ToList();
            
            // first line for numbers
            var numbers = Array.ConvertAll(lines[0].Split(','), s => int.Parse(s));
            
            var field = new (bool, int)[5, 5];
            var fieldRowIndex = 0;

            var bestSteps = 0;
            var answer = 0;

            for (var i = 2; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line == "")
                {
                    continue;
                }
 
                var a = System.Text.RegularExpressions.Regex.Split( line.Trim(), @"\s+");
                var row = Array.ConvertAll(a, s => int.Parse(s));

                for (int j = 0; j < row.Length; j++)
                {
                    field[fieldRowIndex, j] = (false, row[j]);
                }

                fieldRowIndex++;
                
                // finish bingo field
                if (fieldRowIndex == 5)
                {
                    var result = winningField(field, numbers);

                    if (result.Item1 > bestSteps)
                    {
                        bestSteps = result.Item1;
                        answer = result.Item2;
                    }
                    
                    fieldRowIndex = 0;
                }
            }
            
            Console.WriteLine("steps: {0}, answer: {1}", bestSteps, answer);
        }
        
        static (int, int) winningField((bool, int)[,] field, int[] numbers)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);
            var steps = 0;

            foreach (var n in numbers)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        steps += 1;
                        if (field[i, j].Item2 == n)
                        {
                            //Console.WriteLine("Drawing number {0}", n);
                            field[i, j].Item1 = true;
                            //PrintField(field);

                            if (isWinner(field))
                            {
                                var cnt = countNotSet(field);
                                var answer = cnt * n;
                                //Console.WriteLine("Empty sum {0}, last number: {1} = {2}, steps = {3}", cnt, n, answer, steps);
                                return (steps, answer);
                            }
                            
                            break;
                        }
                    }
                }
            }

            return (Int32.MaxValue, 0);
        }

        static int countNotSet((bool, int)[,] field)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);
            var sum = 0;
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j].Item1 == false)
                    {
                        sum += field[i, j].Item2;
                    }
                }
            }

            return sum;
        }

        static bool isWinner((bool, int)[,] field)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);

            // check for winning row
            for (int i = 0; i < height; i++)
            {
                var isRowWinner = true;
                for (int j = 0; j < width; j++)
                {
                    isRowWinner &= field[i, j].Item1;
                }

                if (isRowWinner)
                {
                    return true;
                }
            }

            // check for winning col
            for (int i = 0; i < width; i++)
            {
                // check for winning row
                var isColWinner = true;
                for (int j = 0; j < height; j++)
                {
                    isColWinner &= field[j, i].Item1;
                }

                if (isColWinner)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        static void PrintField((bool, int)[,] field)
        {
            var height = field.GetLength(0);
            var width = field.GetLength(1);
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j].Item1 == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                    Console.Write("{0,2}", field[i, j].Item2);
                    
                    if (field[i, j].Item1 == true)
                    {
                        Console.ResetColor();
                    }
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

    }
}