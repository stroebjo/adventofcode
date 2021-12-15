using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace Day15
{
    class Program
    {
        private static int height;
        private static int width;
        private static int[,] field;
        private static int[,] gScore;
        private static int[,] fScore;
        private static bool[,] flashed;

        static int LowestRisk = int.MaxValue;
        
        static void Main(string[] args)
        {
            LoadField("input.txt");
            AStar();
            LoadField5("input.txt");
            AStar();
        }

        private static void LoadField(String fileName)
        {
            string[] lines = System.IO.File.ReadLines(fileName).ToArray();

            height = lines.Length;
            width = lines[0].Length;

            field = new int[height, width];
            gScore = new int[height, width];
            fScore = new int[height, width];
            flashed = new bool[height, width];

            // build field
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    field[y, x] = lines[y][x] - '0';
                    gScore[y, x] = int.MaxValue;
                    fScore[y, x] = int.MaxValue;
                }
            }
        }

        private static void LoadField5(String fileName)
        {
            string[] lines = System.IO.File.ReadLines(fileName).ToArray();

            height = lines.Length * 5;
            width = lines[0].Length * 5;

            field = new int[height, width];
            gScore = new int[height, width];
            fScore = new int[height, width];
            flashed = new bool[height, width];

            // build field
            for (var y = 0; y < height/5; y++)
            {
                for (var x = 0; x < width/5; x++)
                {
                    for (int repeatY = 0; repeatY < 5; repeatY++)
                    {
                        for (int repeatX = 0; repeatX < 5; repeatX++)
                        {
                            int newX = x + (repeatX*(width/5));
                            int newY = y + (repeatY*(width/5));
                            
                            int val = (lines[y][x] - '0' + repeatX + repeatY);
                            if (val > 9)
                            {
                                val -= 9;
                            }
                            field[newY, newX] = val;
                            gScore[newY, newX] = int.MaxValue;
                            fScore[newY, newX] = int.MaxValue;
                        }      
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

        private static List<int> AllowedLevels = new List<int>();

        static PriorityQueue<(int x, int y), int> open = new PriorityQueue<(int x, int y), int>();
        static List<(int x, int y)> closed = new List<(int x, int y)>();

        private static Dictionary<(int x, int y), (int x, int y)> cameFrom =
            new Dictionary<(int x, int y), (int x, int y)>();

        
        static int h((int x, int y) point)
        {
            return (int) Math.Sqrt((int) Math.Pow(point.x, 2) + (int) Math.Pow(point.y, 2)) * 9;
        }

        static void Path((int x, int y) current)
        {
            var route = new List<(int x, int y)>();
            var totalCost = 0;

            while (cameFrom.Keys.Contains(current))
            {
                route.Add(current);
                flashed[current.x, current.y] = true;
                totalCost += field[current.x, current.y];
                current = cameFrom[current];
            }

            Console.WriteLine(totalCost);
        }
        
        static void AStar()
        {
            open.Enqueue((0, 0), 0);
            (int x, int y) dest = (field.GetLength(1) - 1, field.GetLength(0) - 1);

            gScore[0, 0] = 0;
            fScore[0, 0] = h(open.Peek());

            while (open.Count > 0)
            {
                (int x, int y) current = open.Dequeue();

                if (current.x == dest.x && current.y == dest.y)
                {
                    Path(current);
                    break;
                } 
                
                // foreach neighbor
                var xAxis = new int[] {-1, 0, 1};
                var yAxis = new int[] {-1, 0, 1};

                foreach (var xD in xAxis)
                {
                    foreach (var yD in yAxis)
                    {
                        var xTest = current.x + xD;
                        var yTest = current.y + yD;
                        (int x, int y) nextPoint = (xTest, yTest);

                        if ((0 <= xTest && xTest < field.GetLength(1)) && (0 <= yTest && yTest < field.GetLength(0)))
                        {
                            // no diagonals
                            if (xTest == current.x || yTest == current.y)
                            {
                                var gScoreCurrent = gScore[current.x, current.y] + field[nextPoint.x, nextPoint.y];

                                if (gScoreCurrent < gScore[nextPoint.x, nextPoint.y])
                                {
                                    if (cameFrom.ContainsKey(nextPoint))
                                    {
                                        cameFrom[nextPoint] = current;
                                    }
                                    else
                                    {
                                        cameFrom.Add(nextPoint, current);
                                    }

                                    gScore[nextPoint.x, nextPoint.y] = gScoreCurrent;
                                    fScore[nextPoint.x, nextPoint.y] = gScoreCurrent + h(nextPoint);

                                    if (!closed.Contains(nextPoint))
                                    {
                                        open.Enqueue(nextPoint, fScore[nextPoint.x, nextPoint.y]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Works for example input, but not anything larger...
        /// </summary>
        static void A()
        {
            for (int i = 1; i <= 9; i++)
            {
                AllowedLevels.Add(i);
                
                (int x, int y) start = (0, 0);
                var route = new Stack<(int x, int y)>();
                route.Push(start);
                Search(start.x, start.y, route);
            
                Console.WriteLine(LowestRisk);
            }
        }

        static void Search(int x, int y, Stack<(int x,  int y)> route)
        {
            if (route.Count > 0 && (x == field.GetLength(1) - 1) && y == field.GetLength(0) - 1)
            {
                int sum = 0;
                while (route.Count > 1)
                {
                    var p = route.Pop();
                    sum += field[p.y, p.x];
                }

                if (sum < LowestRisk)
                {
                    Console.WriteLine(LowestRisk);
                    LowestRisk = sum;
                }

                return;
            }
            
            var xAxis = new int[] {-1, 0, 1};
            var yAxis = new int[] {-1, 0, 1};


            var nextPoints = new List<(int, (int x, int y))>();
            
            foreach (var xD in xAxis)
            {
                foreach (var yD in yAxis)
                {
                    var xTest = x + xD;
                    var yTest = y + yD;
                    (int x, int y) nextPoint = (xTest, yTest);
                    
                    if ((0 <= xTest && xTest < field.GetLength(1)) && (0 <= yTest && yTest < field.GetLength(0)))
                    {
                        // no diagonals
                        if (xTest == x || yTest == y)
                        {
                            // not previously visited
                            if (route.Count > 0 && route.Any(p => p.x == xTest && p.y == yTest))
                            {
                                continue;
                            }
                            
                            // allowed safety leve
                            var lvl = field[yTest, xTest];
                            if (!AllowedLevels.Contains(lvl))
                            {
                                continue;
                            }
                            
                            // if current route has higher risk then best kown
                            var nextRisk = lvl + CalculateSafety(route);
                            if (nextRisk >= LowestRisk)
                            {
                                continue;
                            }
                            

                            nextPoints.Add((lvl, nextPoint));
                        }
                    }
                }
            }
            
            // order next points by leve, prefer lower
            nextPoints.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            nextPoints.Reverse();
            
            foreach (var valueTuple in nextPoints)
            {
                var newRoute = new Stack<(int x,  int y)>(new Stack<(int x,  int y)>(route));
                var newPoint = valueTuple.Item2;
                newRoute.Push(newPoint);
                Search(newPoint.x, newPoint.y, newRoute);  
            }
        }

        static int CalculateSafety(Stack<(int x, int y)> route)
        {
            var newRoute = new Stack<(int x,  int y)>(new Stack<(int x,  int y)>(route));

            int sum = 0;
            while (newRoute.Count > 1)
            {
                var p = newRoute.Pop();
                sum += field[p.y, p.x];
            }

            return sum;
        }
    }
}