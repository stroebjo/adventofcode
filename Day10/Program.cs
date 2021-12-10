using System;
using System.Collections;
using System.Collections.Generic;

namespace Day10
{
    class Program
    {
        static Dictionary<char, int> points = new Dictionary<char, int>()
        {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137},
        };
        
        static Dictionary<char, int> pointsForAutocomplete = new Dictionary<char, int>()
        {
            {')', 1},
            {']', 2},
            {'}', 3},
            {'>', 4},
        };
        
        static Dictionary<char, int> dict = new Dictionary<char, int>()
        {
            {'(', 0},
            {'[', 1},
            {'{', 2},
            {'<', 3},
            {')', 4},
            {']', 5},
            {'}', 6},
            {'>', 7},
        };
            
        static Dictionary<char, char> pairs = new Dictionary<char, char>()
        {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'},
        };
        
        static void Main(string[] args)
        {
            A();
            B();
        }

        static void A()
        {
            int errorScore = 0;
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                Stack<char> nextClose = new Stack<char>();
                
                for (var i = 0; i < line.Length; i++)
                {
                    var c = line[i];
                    var bracket = dict[c];

                    if (bracket < 4)
                    {
                        nextClose.Push(pairs[c]);
                    }
                    else
                    {
                        if (c  != nextClose.Pop()) 
                        {
                            errorScore += points[c];
                        }
                    }
                }
            }
            
            Console.WriteLine("A) Error score: {0}", errorScore);
        }

        static void B()
        {
            List<long> autocompleteScores = new List<long>();
            
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                Stack<char> nextClose = new Stack<char>();
                var isBroken = false; // no break labels in C# :(
                
                for (var i = 0; i < line.Length; i++)
                {
                    var c = line[i];
                    var bracket = dict[c];

                    if (bracket < 4)
                    {
                        nextClose.Push(pairs[c]);
                    }
                    else
                    {
                        if (c != nextClose.Pop()) 
                        {
                            isBroken = true;
                            break;
                        }
                    }
                }
                
                // autocomplete only if it's not broken
                if (isBroken)
                {
                    continue;
                }

                long score = 0;
                while(nextClose.Count > 0) // no char c = s.Pop() in C# since assignment return value, not boolean!
                {
                    char c = nextClose.Pop();
                    score = score * 5 + pointsForAutocomplete[c];
                }
                
                autocompleteScores.Add(score);
            }
            
            autocompleteScores.Sort();
            Console.WriteLine("B) Autocomplete score: {0}", autocompleteScores[(autocompleteScores.Count / 2)]);
        }
    }
}