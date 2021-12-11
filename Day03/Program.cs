using System;
using System.Collections.Generic;

namespace Day3
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
            var pos = 12;
            
            var bits0 = new int[pos];
            var bits1 = new int[pos];

            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '0')
                    {
                        bits0[i] += 1;
                    }
                    else
                    {
                        bits1[i] += 1;
                    }
                }
            }
            
            Console.WriteLine("[{0}]", string.Join(", ", bits0));
            Console.WriteLine("[{0}]", string.Join(", ", bits1));
            
            var gamma = 0;
            var epsilon = 0;

            int j = 0;
            for (int i = bits0.Length-1; i >= 0 ; i--)
            {
                if (bits1[i] > bits0[i])
                {
                    var xx = 1 << j;
                    gamma = gamma | xx;
                }

                j += 1;
            }
            epsilon = (~gamma) & (0b111111111111);
            Console.WriteLine("Gamma: {0}", gamma);
            Console.WriteLine("Epsiolon: {0}", epsilon);
            Console.WriteLine("Power consumption: {0}", epsilon * gamma);
        }
        
        static void B()
        {
            var lines = new List<string>();
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                lines.Add(line);
            }

            var oxygen = findOxygen(lines, 0);
            var co2 = findCo2(lines, 0);
            
            var oxygenscrubber = Convert.ToInt32(oxygen, 2);
            var co2scrubber = Convert.ToInt32(co2, 2);
            
            var lifeSupport = oxygenscrubber * co2scrubber;
            Console.WriteLine("{0} x {1} = {2}", oxygenscrubber, co2scrubber, lifeSupport);
        }

        private static string findOxygen(List<string> numbers, int i)
        {
            var bits = countBitsAtPos(numbers);
            
            var bit = 0;
            if (bits[i].Item2 >= bits[i].Item1)
            {
                bit = 1;
            }

            var newList = new List<string>();
            for (int j = 0; j < numbers.Count; j++)
            {
                var bitToCheck = numbers[j][i] - '0'; // convert char back to int
                if (bit == bitToCheck)
                {
                    newList.Add(numbers[j]);
                }
            }

            if (newList.Count == 1)
            {
                return newList[0];
            }

            return findOxygen(newList, i+1);
        }

        private static string findCo2(List<string> numbers, int i)
        {
            var bits = countBitsAtPos(numbers);
            
            var bit = 1;
            if (bits[i].Item1 <= bits[i].Item2)
            {
                bit = 0;
            }

            var newList = new List<string>();
            for (int j = 0; j < numbers.Count; j++)
            {
                var bitToCheck = numbers[j][i] - '0'; // convert char back to int
                if (bit == bitToCheck)
                {
                    newList.Add(numbers[j]);
                }
            }

            if (newList.Count == 1)
            {
                return newList[0];
            }
            
            return findCo2(newList, i+1);
        }

        private static (int, int)[] countBitsAtPos(List<string> numbers)
        {
            var bits = new(int, int)[numbers[0].Length];

            foreach (var el in numbers)
            {
                for (int i = 0; i < el.Length; i++)
                {
                    if (el[i] == '0')
                    {
                        bits[i].Item1 += 1;
                    }
                    else
                    {                        
                        bits[i].Item2 += 1;
                    }
                }
            }

            return bits;
        }
    }
}