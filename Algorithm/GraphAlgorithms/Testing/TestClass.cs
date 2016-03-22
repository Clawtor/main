using GraphAlgorithms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestFixture]
    public class TestClass
    {
        [TestCase]
        public void TestBFS()
        {
            var testGraphs = GenerateTestGraphs();
            var test1 = testGraphs["hub"].BFS("B");
            var test2 = testGraphs["tree"].BFS("A");
            var test3 = testGraphs["cycles"].BFS("A");

			foreach(var v in test1)
			{
				Console.WriteLine(v);
			}

			Console.ReadLine();
        }

        [TestCase]
        public void TestDFS()
        {
            var testGraphs = GenerateTestGraphs();

        }

        private Dictionary<string, Graph> GenerateTestGraphs()
        {
            Dictionary<string, Graph> dict = new Dictionary<string, Graph>();

            var g1 = new Graph();
            g1.AddVertex("A");
            g1.AddVertex("B");
            g1.AddVertex("C");
            g1.AddVertex("D");
            g1.AddVertex("E");

            g1.AddEdge("A", "B");
            g1.AddEdge("A", "C");
            g1.AddEdge("A", "D");
            g1.AddEdge("A", "E");

            dict.Add("hub", g1);

            var g2 = new Graph();
            g2.AddVertex("A");
            g2.AddVertex("B");
            g2.AddVertex("C");
            g2.AddVertex("D");
            g2.AddVertex("E");
            g2.AddVertex("F");
            g2.AddVertex("G");

            g2.AddEdge("A", "B");
            g2.AddEdge("A", "C");
            g2.AddEdge("B", "D");
            g2.AddEdge("B", "E");
            g2.AddEdge("C", "F");
            g2.AddEdge("C", "G");

            dict.Add("tree", g2);

            var g3 = new Graph();

            g3.AddVertex("A");
            g3.AddVertex("B");
            g3.AddVertex("C");
            g3.AddVertex("D");
            g3.AddVertex("E");
            g3.AddVertex("F");

            g3.AddEdge("A", "B");
            g3.AddEdge("A", "C");
            g3.AddEdge("B", "C");
            g3.AddEdge("B", "D");
            g3.AddEdge("D", "E");
            g3.AddEdge("E", "C");
            g3.AddEdge("C", "F");
            g3.AddEdge("A", "F");

            dict.Add("cycles", g3);
            return dict;
        }
    }
}
