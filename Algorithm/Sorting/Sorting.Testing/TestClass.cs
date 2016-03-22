using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting.Testing
{
    [TestFixture]
    public class TestClass
    {
        /// <summary>
        /// Add random integers to heap and test children are always 
        /// greater than their parents.
        /// </summary>
        [TestCase]
        public void TestAdd()
        {
            Heap heap = new Heap();
            Random random = new Random();
            List<int> randomNumbers = Enumerable.Range(0, 10)
                .Select(x => random.Next(100)).ToList();

            foreach (var num in randomNumbers)
            {
                heap.Add(num);
            }
            List<int> heapData = heap.ToList();
            foreach(var num in heapData)
            {
                if(heap.GetLeftChild(num) != null)
                     Assert.LessOrEqual(num, heap.GetLeftChild(num));
                if (heap.GetRightChild(num) != null)
                    Assert.LessOrEqual(num, heap.GetRightChild(num));
            }
        }

        [TestCase]
        public void TestExtract()
        {
            Heap heap = new Heap();
            Random random = new Random();
            int size = 10000;
            List<int> randomNumbers = Enumerable.Range(0, size)
                .Select(x => random.Next(100)).ToList();

            foreach (var num in randomNumbers)
            {
                heap.Add(num);
            }
            List<int> sortedNums = new List<int>();
            DateTime before = DateTime.Now;
            for(int i=0;i< size;i++)
            {
                sortedNums.Add(heap.ExtractRoot().Value);
            }
            var elapsed = (DateTime.Now - before);

        }
    }
}
