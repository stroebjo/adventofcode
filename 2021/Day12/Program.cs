using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        
        private static List<KeyValuePair<string, string>> edges = new List<KeyValuePair<string, string>>();
        private static List<List<string>> solutions = new List<List<string>>();
        private static List<string> StartEnd = new List<string>() {"start", "end"};

        static void Main(string[] args)
        {
            LoadEdges(@"input.txt");
            A();
            B();
        }
        
        private static void LoadEdges(String file)
        {
            foreach (string line in System.IO.File.ReadLines(file))
            {
                var parts = line.Split('-');
                edges.Add(new KeyValuePair<string, string>(parts[0], parts[1])); // A -> B
                edges.Add(new KeyValuePair<string, string>(parts[1], parts[0])); // B -> A
            }
        }

        static void A()
        {
            FindPath("start",  new List<string>() {"start"}, true);
            Console.WriteLine($"A) {solutions.Count}");
        }
        
        static void B()
        {
            solutions = new List<List<string>>();
            FindPath("start", new List<string>() {"start"});
            Console.WriteLine($"B) {solutions.Count}");
        }
        
        static public void FindPath(string start, List<string> visited, bool hasDuplicate = false)
        {
            var nextEdges = edges.Where(x => x.Key.Equals(start)).Select(x => x.Value);

            foreach (var e in nextEdges)
            {
                var visitedCurrent = new List<string>(visited);
                
                if (e.All(char.IsLower) && visitedCurrent.Contains(e))
                {
                    if (!hasDuplicate && !StartEnd.Contains(e))
                    {
                        visitedCurrent.Add(e);
                        FindPath(e, visitedCurrent, true);
                    }
                    
                    continue;
                }
                
                visitedCurrent.Add(e);

                if (e.Equals("end"))
                {
                    solutions.Add(visitedCurrent);
                    continue;
                }
                
                FindPath(e, visitedCurrent, hasDuplicate);
            }
        }
    }
}