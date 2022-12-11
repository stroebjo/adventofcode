using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            A();
            B();
        }

        static Dictionary<char, int> count = new Dictionary<char, int>();
        static Dictionary<string, string> insertions = new Dictionary<string, string>();

        static void A()
        {
            var polymer = "";
            var insertions = new Dictionary<string, string>();
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                if (line == "")
                {
                    continue;
                }
                
                var split = line.Split(" -> ");

                if (split.Length == 1)
                {
                    polymer = line;
                }
                else
                {
                    insertions.Add(split[0], split[1]);
                }
            }

            for (int s = 0; s < 10; s++)
            {
                var newPolymer = "";
                for (int i = 0; i < polymer.Length - 1; i++)
                {
                    var pair = new char[] {polymer[i], polymer[i + 1]}; // is there a C#'er way?
                    var pair2 = new string(pair);
                    newPolymer += polymer[i] + insertions[pair2];
                }
                
                var endC = new char[] { polymer[^1]}; // is there a C#'er way?
                var end = new string(endC);
                newPolymer+= end;
                polymer = newPolymer;
            }

            var quantities = new List<int>();
            var count = polymer.Distinct();
            
            foreach (var c2c in count)
            {
                quantities.Add(polymer.ToCharArray().Count(c => c == c2c));
            }
            
            Console.WriteLine(quantities.Max() - quantities.Min());
        }
        
        static void B()
        {
            var polymer = "";
            foreach (string line in System.IO.File.ReadLines(@"input.txt"))
            {
                if (line == "")
                {
                    continue;
                }
                
                var split = line.Split(" -> ");

                if (split.Length == 1)
                {
                    polymer = line;
                }
                else
                {
                    count.TryAdd(split[1][0], 0);
                    insertions.Add(split[0], split[1]);
                }
            }
            
            // count starting values
            var result = new Dictionary<char, long>();
            for (int i = 0; i < polymer.Length; i++)
            {
                result.TryAdd(polymer[i], 0);
                result[polymer[i]] += 1;
            }
            
            for (int i = 0; i < polymer.Length - 1; i++)
            {
                var r = CountChars(polymer[i], polymer[i + 1], 40);
                foreach (var kvp in r)
                {
                    result.TryAdd(kvp.Key, 0);
                    result[kvp.Key] += kvp.Value;
                }
            }
            
            var charCounts = result.Values.ToList();
            Console.WriteLine(charCounts.Max() - charCounts.Min());
        }
        
        private static Dictionary<KeyValuePair<string, int>, Dictionary<char, long>> cache =
            new Dictionary<KeyValuePair<string, int>, Dictionary<char, long>>();

        static Dictionary<char, long> CountChars(char a, char b, int steps)
        {
            var counts = new Dictionary<char, long>();
            var pair = new string(new[] {a, b});
            var newChar = insertions[pair][0];
            var key = new KeyValuePair<string, int>(pair, steps);
            
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            
            counts.TryAdd(newChar, 0);
            counts[newChar] += 1;

            var nextStep = steps - 1;

            if (steps > 1)
            {
                var aRec = CountChars(a, newChar, nextStep);
                var bRec = CountChars(newChar, b, nextStep);

                // merge
                foreach (var kvp in aRec)
                {
                    counts.TryAdd(kvp.Key, 0);
                    counts[kvp.Key] += kvp.Value;
                }
                foreach (var kvp in bRec)
                {
                    counts.TryAdd(kvp.Key, 0);
                    counts[kvp.Key] += kvp.Value;
                }
                
                cache.TryAdd(key, counts);
            }

            return counts;
        }
    }
}