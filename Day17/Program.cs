using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Day17
{
    class Program
    {
        
        private static (int min, int max) targetY;
        private static (int min, int max) targetX;

        private static int maxY = int.MinValue;
        private static int count = 0;
        
        static void Main(string[] args)
        {
            // target area: x=128..160, y=-142..-88
            targetX = (128, 160);
            targetY = (-142, -88);
            
            // test input
            // target area: x=20..30, y=-10..-5
            //targetX = (20, 30);
            //targetY = (-10, -5);
            
            for (int startX = -100; startX <= targetX.max; startX++)
            {
                for (int startY = -500; startY < 500; startY++)
                {
                    Shoot(startX, startY);
                }
            }
            
            Console.WriteLine(maxY);
            Console.WriteLine(count);
        }
        
        static void Shoot(int xVelocity, int yVelocity)
        {
            (int x, int y) position = (0, 0);
            int maxYRun = 0;
            
            while (true)
            {
                position.x += xVelocity;
                position.y += yVelocity;

                if (xVelocity > 0)
                {
                    xVelocity -= 1;
                }
                else if (xVelocity < 0)
                {
                    xVelocity += 1;
                }

                yVelocity -= 1;
        
                // overshoot?
                if (position.x > targetX.max || position.y < targetY.min)
                {
                    return;
                } 
                
                if (position.y > maxYRun)
                {
                    maxYRun = position.y;
                }

                // in target area?
                if (targetX.min <= position.x && position.x <= targetX.max &&
                    targetY.min <= position.y && position.y <= targetY.max)
                {
                    count += 1;
                    
                    if (maxYRun > maxY)
                    {
                        maxY = maxYRun;
                    }

                    return;
                }
            }
        }
    }
}