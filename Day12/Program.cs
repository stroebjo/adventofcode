using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        
        private static List<KeyValuePair<string, string>> edges = new List<KeyValuePair<string, string>>();
        private static List<List<string>> solutions = new List<List<string>>();
        
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
            FindPath("start",  new List<string>() {"start"});
            Console.WriteLine(solutions.Count);
        }

        static public void FindPath(string start, List<string> visited)
        {
            var nextEdges = edges.Where(x => x.Key.Equals(start)).Select(x => x.Value);

            var spaces = "".PadRight(visited.Count);
            
            //Console.WriteLine($"{spaces} am at {start}, next options: {string.Join(", ", nextEdges)}");
            
            foreach (var e in nextEdges)
            {
                //Console.WriteLine($"{spaces} going to -> {e}");

                var v2 = new List<string>();
                foreach (var s in visited)
                {
                    v2.Add(s);
                }
                
                if (e.All(char.IsLower) && v2.Contains(e))
                {
                    //Console.WriteLine($"{spaces} cant go to {e} />");
                    // cant visit same small cave twice, skip this route 
                    continue;
                }
                
                v2.Add(e);

                if (e.Equals("end"))
                {
                    solutions.Add(v2);
                    continue;
                }
                
                FindPath(e, v2);
            }
        }

        static void B()
        {
            solutions = new List<List<string>>();
            FindPathB("start", new List<string>() {"start"});
            Console.WriteLine(solutions.Count);
        }
        
        static public void FindPathB(string start, List<string> visited, string smallDouble = "")
        {
            var nextEdges = edges.Where(x => x.Key.Equals(start)).Select(x => x.Value);

            foreach (var e in nextEdges)
            {
                var v2 = new List<string>();
                foreach (var s in visited)
                {
                    v2.Add(s);
                }
                
                if (e.All(char.IsLower) && v2.Contains(e))
                {
                    var nope = new List<string>() {"start", "end"};

                    if (!nope.Contains(e) && smallDouble.Equals(""))
                    {
                        v2.Add(e);
                        FindPathB(e, v2, e);
                    }
                    
                    continue;
                }
                
                v2.Add(e);

                if (e.Equals("end"))
                {
                    solutions.Add(v2);
                    continue;
                }
                
                FindPathB(e, v2, smallDouble);
            }
        }
    }
}