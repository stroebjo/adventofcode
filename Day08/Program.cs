using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Day8
{
    class Program
    {
        private static readonly int[] UniqueSegments = {2, 4, 3, 7}; // Digits 1, 4, 7, 8
        
        static void Main(string[] args)
        {
            A();
            B();
        }

        static void A()
        {
            int unique = 0;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                var split = line.Split(" | ");
                var input = split[0].Trim();
                var output = split[1].Trim();

                var outputDigits = output.Split(' ');
                
                foreach (var digit in outputDigits)
                {
                    var l = digit.Length;

                    if (UniqueSegments.Contains(l))
                    {
                        unique += 1;
                    }
                }
            }

            Console.WriteLine(unique);
        }

        private static int[] DIGITS = {0x7E, 0x30, 0x6D, 0x79, 0x33, 0x5B, 0x5F, 0x70, 0x7F, 0x7B};

        static int Segment2Digit(int seg, int[] mapping)
        {
            for (int i = 0; i < 10; i++)
            {
                if ((seg ^ mapping[i]) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        static void B()
        {
            List<char> indizes = new List<char>{'a', 'b', 'c', 'd', 'e', 'f', 'g'};
            int sum = 0;

            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                var split = line.Split(" | ");
                var inputs = split[0].Trim().Split(' ');
                var output = split[1].Trim().Split(' ');
                
                var inputBinary = new int[inputs.Length];
                
                // input to binary
                for (var i = 0; i < inputs.Length; i++)
                {
                    var input = inputs[i];
                    var binary = 0;
                    
                    for (int j = 0; j < input.Length; j++)
                    {
                        binary |= (1 << indizes.IndexOf(input[j]));
                    }

                    inputBinary[i] = binary;
                }
                
                // determine mapping
                int[] mapping = new int[10];

                // find digit 1, only digit with 2 segments
                foreach (var b in inputBinary)
                {
                    if (NumberOfSetBits(b) != 2)
                    {
                        continue;
                    }

                    mapping[1] = b;
                    break;
                }
                
                // find digit 4, only digit with 4 segments
                foreach (var b in inputBinary)
                {
                    if (NumberOfSetBits(b) != 4)
                    {
                        continue;
                    }

                    mapping[4] = b;
                    break;
                }

                // find digit 7, only digit with 3 segments
                foreach (var b in inputBinary)
                {
                    if (NumberOfSetBits(b) != 3)
                    {
                        continue;
                    }

                    mapping[7] = b;
                    break;
                }
                
                // find digit 8, only segment with 7 segments 
                foreach (var b in inputBinary)
                {
                    if (NumberOfSetBits(b) != 7)
                    {
                        continue;
                    }

                    mapping[8] = b;
                    break;
                }
                
                // Segment A
                var A = mapping[7] ^ mapping[1];
                var Aand4and1 = A | mapping[4] | mapping[1];
                
                // find D (lower part of 5)
                var D = 0;
                foreach (var b in inputBinary)
                {
                    if (NumberOfSetBits(b) != 5)
                    {
                        continue;
                    }

                    var r = b | Aand4and1;
                    r = r ^ Aand4and1;
                    
                    if (NumberOfSetBits(r) == 1)
                    {
                        D = r;
                        break;
                    }
                }
                
                // Segment E
                int E = 0;
                E = (mapping[8] ^ (mapping[4] | D | A));
                
                // digit 9
                mapping[9] = mapping[8] ^ E;

                // digit 6
                foreach (var b in inputBinary)
                {
                    if (NumberOfSetBits(b) != 6)
                    {
                        continue;
                    }
                    // 6 bits mean 6 or 0 or 9
                    
                    // already know 9!
                    if ((b ^ mapping[9]) == 0)
                    {
                        continue;
                    }

                    // for 0 this has to be 2 set segments! for 6 1 segment
                    var t = b & mapping[1];

                    if (NumberOfSetBits(t) == 2)
                    {
                        mapping[0] = b;
                    }
                    else
                    {
                        mapping[6] = b;
                    }
                    // we could check if both mapping are set to break the loop...
                }
                
                // left border top/bottom (F and E)
                // 0 - 7 - D
                var leftFE = mapping[0] ^ (mapping[7] | D);

                // 3 ist 8 without FE
                mapping[3] = mapping[8] ^ leftFE;

                //  2 and 5
                foreach (var b in inputBinary)
                {
                    if (NumberOfSetBits(b) != 5)
                    {
                        continue;
                    }

                    if ((b ^ mapping[3]) == 0)
                    {
                        continue; // 3 has 5 segments, is already known.
                    }
                    
                    // 2 + 9  is 7 segments
                    // 5 + 9 would be 6 segments
                    var m = b | mapping[9];

                    if (NumberOfSetBits(m) == 7)
                    {
                        mapping[2] = b;
                        continue;
                    } 
                    
                    if (NumberOfSetBits(m) == 6)
                    {
                        mapping[5] = b;
                    }
                }
                
                // convert value to binary
                var valueBinary = new int[output.Length];
                for (var i = 0; i < output.Length; i++)
                {
                    var input = output[i];
                    var binary = 0;

                    for (int j = 0; j < input.Length; j++)
                    {
                        var wtf = (1 << indizes.IndexOf(input[j]));
                        binary |= wtf;
                    }

                    valueBinary[i] = binary;
                }
                
                // identify output value and sum digits with corresponding 10^x
                int exponent = valueBinary.Length;
                int value = 0;
                for (var i = 0; i < valueBinary.Length; i++)
                {
                    value += Segment2Digit(valueBinary[i], mapping) * (int) Math.Pow(10, --exponent);
                }
                
                sum += value;
            }

            Console.WriteLine($"Sum {sum}");
        }
        
        static int NumberOfSetBits(int i)
        {
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }
        
        static void Test()
        {
            Console.WriteLine(Segment2Digit(0x7E, DIGITS));
            Console.WriteLine(Segment2Digit(0x30, DIGITS));
            Console.WriteLine(Segment2Digit(0x6D, DIGITS));
            Console.WriteLine(Segment2Digit(0x79, DIGITS));
            Console.WriteLine(Segment2Digit(0x33, DIGITS));
            Console.WriteLine(Segment2Digit(0x5B, DIGITS));
            Console.WriteLine(Segment2Digit(0x5F, DIGITS));
            Console.WriteLine(Segment2Digit(0x70, DIGITS));
            Console.WriteLine(Segment2Digit(0x7F, DIGITS));
            Console.WriteLine(Segment2Digit(0x7B, DIGITS));
        }
    }
}