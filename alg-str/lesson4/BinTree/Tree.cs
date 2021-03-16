using System;
using System.Collections.Generic;

namespace BinTree
{
    using System.Collections;
    using System.Threading.Tasks;
    public class Tree<T> : ICollection<T>, ICollection
        where T : IComparable<T>, IEquatable<T>
    {

        public TreeNode<T> Root { get; private set; }
        public int Count { get; private set; }


        public bool IsReadOnly { get { return false; } }
        public bool IsSynchronized { get { return false; } }
        public object SyncRoot => throw new NotImplementedException();

        //exception messages
        internal const string ITEM_NULL = "The item argument cannot be null.";

        public Tree()
        {

        }

        public Tree(params T[] item)
        {
            for (int i = 0; i < item.Length; i++, Count++)
                Add(item[i]);
        }


        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(ITEM_NULL, new NullReferenceException("item"));

            Count++;
            //add root
            if (Root is null)
            {
                Root = new TreeNode<T> { item = item };
                return;
            }


            var temp = Root;
            int compare;

            while (true)
            {
                compare = item.CompareTo(temp.item);
                if (compare == 0) return;
                if (compare < 0)
                {
                    if (temp.Left is null)
                    {
                        temp.Left = new TreeNode<T> { item = item, Parent = temp };
                        break;
                    }
                    else temp = temp.Left;
                }
                else
                {
                    if (temp.Right is null)
                    {
                        temp.Right = new TreeNode<T> { item = item, Parent = temp };
                        break;
                    }
                    else temp = temp.Right;
                }
            }

        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item) => Remove(Search(item));
         
        public bool Remove(TreeNode<T> item)
        {
            if (item is null) return false;
            // item is leaf
            if (item.Left is null && item.Right is null)
            {
                if (Object.ReferenceEquals())
                {

                }
            }
        }


        public TreeNode<T> Search(T item)
        {
            if (item is null) throw new ArgumentNullException(ITEM_NULL, new NullReferenceException("item"));
            var temp = Root;
            while (!(temp is null))
            {
                if (item.Equals(temp.item)) return temp;
                if (item.CompareTo(temp.item) < 0) temp = temp.Left;
                else temp = temp.Right;
            }
            return temp;
        }

        public bool Contains(T item)
        {
            return Search(item) is null ? false : true;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }



        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
