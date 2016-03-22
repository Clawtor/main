using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class Heap
    {
        List<int> data;
        public Heap(int? root = null)
        {
            data = new List<int>();
            if (root != null)
                Add(root.Value);
        }

        public int Count()
        {
            if (data != null)
            {
                return data.Count;
            }
            return 0;
        }

        public int? ExtractRoot()
        {
            if (Count() == 0)
            {
                return null;
            }
            //  Get root;
            int root = data[0];
            //  Set root to last element.
            data[0] = data[data.Count-1];
            HeapifyDown(0);
            return root;
        }

        //  Adds as child to last element.
        //  Then heapify up.
        public void Add(int value)
        {
            data.Add(value);
            HeapifyUp(Count()-1);
        }
        
        private void HeapifyUp(int index)
        {
            if (index > 0)
            {
                var parentIndex = GetParent(index);
                if (parentIndex == null) return;

                if(data[index] < data[parentIndex.Value])
                {
                    Swap(index, parentIndex.Value);
                    HeapifyUp(parentIndex.Value);
                }
            }
        }

        private void HeapifyDown(int index)
        {
            int j;
            //  Checks if index is a leaf.
            if (2 * index >= Count())
            {
                return;
            } else
            if (2 * index < Count())
            {
                var left = GetLeftChild(index);
                var right = GetRightChild(index);
                if (left.HasValue)
                {
                    if (right.HasValue)
                    {
                        j = data[left.Value] < data[right.Value] ? left.Value : right.Value;
                    }
                    else
                    {
                        j = left.Value;
                    }
                }
                else
                {
                    j = right.Value;
                }
            }
            else
            {
                j = 2 * index;
            }
            if(data[j] < data[index])
            {
                Swap(index, j);
                HeapifyDown(j);
            }
        }
        
        public List<int> ToList()
        {
            List<int> copy = new List<int>();
            data.ForEach(x => copy.Add(x));
            return copy;
        }

        #region Helper functions
        private void Swap(int i, int j)
        {
            var temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }

        public int? GetLeftChild(int index)
        {
            if (data.Count < (index + 1) * 2 - 1)
            {
                return null;
            }
            else
            {
                return (index + 1) * 2 - 1;
            }
        }
        public int? GetRightChild(int index)
        {
            if (data.Count <= (index + 1) * 2)
            {
                return null;
            }
            else
            {
                return (index+1) * 2 ;
            }
        }
        private int? GetParent(int index)
        {
            if (index <= data.Count)
            {
                return (int)Math.Floor(index / 2.0);
            }
            return null;
        }

        #endregion
    }
}
