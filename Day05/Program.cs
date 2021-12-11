using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            A();
            B();
        }

        class Point
        {
            public int X;
            public int Y;
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return String.Format("{0},{1}", X, Y);
            }
        }
        
        static void A()
        {
            List<(Point, Point)> pp = new List<(Point, Point)>();
            int maxX = 0;
            int maxY = 0;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                var coords = line.Replace(" -> ", ",").Split(",").Select(int.Parse).ToArray();

                if ((coords[0] == coords[2]) || (coords[1] == coords[3]))
                {
                    pp.Add((new Point(coords[0], coords[1]), new Point(coords[2], coords[3])));
                    maxX = Math.Max(Math.Max(coords[0], coords[2]), maxX);
                    maxY = Math.Max(Math.Max(coords[1], coords[3]), maxY);
                }
            }
            
            int[,] field = new int[maxX+1,maxY+1];
            
            foreach (var line in pp)
            {
                var p1 = line.Item1;
                var p2 = line.Item2;

                if (p1.X == p2.X)
                {
                    var startY = Math.Min(p1.Y, p2.Y);
                    var endY = Math.Max(p1.Y, p2.Y);

                    for (int y = startY; y <= endY; y++)
                    {
                        field[p1.X, y] += 1;
                    }
                }
                else
                {
                    var startX = Math.Min(p1.X, p2.X);
                    var endX = Math.Max(p1.X, p2.X);

                    for (int x = startX; x <= endX; x++)
                    {
                        field[x, p1.Y] += 1;
                    }
                }
            }
            
            // count > 2
            int cnt = 0;
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y] >= 2)
                    {
                        cnt += 1;
                    }
                }
            }
            
            Console.WriteLine("Numbers of >=2 = {0}", cnt);
        }

        static void B()
        {
            List<(Point, Point)> pp = new List<(Point, Point)>();
            int fieldX = 0;
            int fieldY = 0;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                var coords = line.Replace(" -> ", ",").Split(",").Select(int.Parse).ToArray();

                var p1 = new Point(coords[0], coords[1]);
                var p2 = new Point(coords[2], coords[3]);

                if (p1.X < p2.X)
                {
                    pp.Add((p1, p2));
                }
                else
                {
                    pp.Add((p2, p1));
                }
                
                fieldX = Math.Max(Math.Max(coords[0], coords[2]), fieldX);
                fieldY = Math.Max(Math.Max(coords[1], coords[3]), fieldY);
            }
            
            int[,] field = new int[fieldX+1,fieldY+1];
            
            foreach (var line in pp)
            {
                var p1 = line.Item1;
                var p2 = line.Item2;
                
                var xDelta = (p1.X == p2.X) ? 0 : ((p1.X > p2.X) ? -1 : 1);
                var yDelta = (p1.Y == p2.Y) ? 0 : ((p1.Y > p2.Y) ? -1 : 1);
                
                //Console.WriteLine("From {0} -> {1}", p1, p2);

                int x = p1.X;
                int y = p1.Y;

                while (true)
                {
                    field[x, y] += 1;

                    if (p2.X == x && p2.Y == y)
                    {
                        break;
                    }

                    x += xDelta;
                    y += yDelta;
                }
            }
            
            // count > 2
            int cnt = 0;
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    
                    
                    if (field[x, y] >= 2)
                    {
                        cnt += 1;
                    }
                }
            }
            
            //PrintField(field);
            Console.WriteLine("Numbers of >=2 = {0}", cnt);
        }

        static void PrintField(int[,] field)
        {
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[y, x] == 0)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(field[y, x]);
                    }
                }
                Console.Write("\n");
            }
        }
    }
}