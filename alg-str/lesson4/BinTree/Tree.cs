using System;
using System.Collections.Generic;

namespace BinTree
{
    using System.Collections;

    public class Tree<T> : ICollection<T>, ICollection
        where T : IComparable<T>, IEquatable<T>
    {
        public TreeNode<T> Root { get; private set; }
        public int Count { get; private set; }


        public bool IsReadOnly { get { return false; } }
        public bool IsSynchronized { get { return false; } }
        private Object _syncRoot;
        public object SyncRoot
        {
            get
            {
                if (_syncRoot is null)
                    System.Threading.Interlocked.CompareExchange<Object>(ref _syncRoot, new Object(), null);
                return _syncRoot;
            }
        }

        //exception messages
        internal const string ARGUMENT_NULL = "The argument cannot be null.";


        #region ctor, Add

        public Tree()
        {

        }

        public Tree(IEnumerable<T> collection)
        {
            if (collection is null) throw new ArgumentNullException(ARGUMENT_NULL, new NullReferenceException("collection"));
            var en = collection.GetEnumerator();
            while (en.MoveNext())
                Add(en.Current);
        }

        public Tree(params T[] item)
        {
            for (int i = 0; i < item.Length; i++)
                Add(item[i]);
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(ARGUMENT_NULL, new NullReferenceException("item"));
            //add root
            if (Root is null)
                Root = new TreeNode<T> { Item = item };
            else
            {
                var temp = Root;
                int compare;
                int height = 0;
                while (true)
                {
                    height++;
                    compare = item.CompareTo(temp.Item);
                    if (compare == 0) return;  // return if item alredy exist
                    if (compare < 0)
                    {
                        if (temp.Left is null)
                        {
                            temp.Left = new TreeNode<T> { Item = item, Parent = temp, Height = height };
                            break;
                        }
                        else temp = temp.Left;
                    }
                    else
                    {
                        if (temp.Right is null)
                        {
                            temp.Right = new TreeNode<T> { Item = item, Parent = temp, Height = height };
                            break;
                        }
                        else temp = temp.Right;
                    }
                }
            }
            Count++;
        }
        #endregion

        #region Balancing

        internal static void Balance(TreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        /// <summary>Left node cannot be null.</summary>
        internal static void RightRotation(TreeNode<T> node)
        {
            var temp = node.Parent;
            var succesor = node.Left;

            node.Parent = succesor;
            succesor.Parent = temp;
            if (temp is not null)
            {
                if (succesor.Item.CompareTo(temp.Item) < 0)
                    temp.Left = succesor;
                else temp.Right = succesor;
            }

            temp = succesor.Right;
            succesor.Right = node;
            node.Left = temp;
            if (temp is not null) temp.Parent = node;

        }

        /// <summary>Right node cannot be null.</summary>
        internal static void LeftRotation(TreeNode<T> node)
        {
            var temp = node.Parent;
            var succesor = node.Right;

            node.Parent = succesor;
            succesor.Parent = temp;
            if (temp is not null)
            {
                if (succesor.Item.CompareTo(temp.Item) < 0)
                    temp.Left = succesor;
                else temp.Right = succesor;
            }

            temp = succesor.Left;
            succesor.Left = node;
            node.Right = temp;
            if (temp is not null) temp.Parent = node;
        }

        internal static int BalanceFactor(TreeNode<T> node)
        {
            return node is null ? 0 : node.Left?.Height ?? 0 - node.Right?.Height ?? 0;
        }

        internal static int MaxHeight(TreeNode<T> node)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Remove
        internal static void Invalidate(TreeNode<T> node)
        {
            if (node is null) return;
            node.Parent = null;
            node.Left = null;
            node.Right = null;
        }

        public void Clear()
        {
            var enBFS = BFS();
            while (enBFS.MoveNext())
                Invalidate(enBFS.Current);

            Count = 0;
            Root = null;
        }

        public bool Remove(T item) => Remove(Search(item));

        public bool Remove(TreeNode<T> node)
        {
            if (node is null) return false;
            Count--;

            TreeNode<T> successor;
            TreeNode<T> parent;

            //leaf
            if (node.Left is null && node.Right is null)
            {
                parent = node.Parent;
                if (parent is null) // root is leaf
                    Root = null;
                else
                {
                    if (node.Item.CompareTo(parent.Item) < 0)
                        parent.Left = null;
                    else
                        parent.Right = null;

                    node.Parent = null;
                }
                return true;
            }

            // one child
            if (node.Left is null || node.Right is null)
            {
                successor = node.Left is null ? node.Right : node.Left;
                parent = node.Parent;

                if (parent is null) // root one child
                    Root = successor;
                else
                {
                    if (node.Item.CompareTo(parent.Item) < 0)
                        parent.Left = successor;
                    else
                        parent.Right = successor;
                }

                successor.Parent = parent;
                Invalidate(node);
                return true;
            }

            // two child
            successor = FindMin(node.Right); // right successor
            //remove right successor
            parent = successor.Parent;
            if (successor.Right is not null) successor.Right.Parent = parent;
            if (successor.Item.CompareTo(parent.Item) < 0)
                parent.Left = successor.Right;
            else parent.Right = successor.Right;

            //swap
            node.Item = successor.Item;
            Invalidate(successor);
            return true;
        }
        #endregion

        #region Search
        internal static TreeNode<T> FindMin(TreeNode<T> startNode)
        {
            while (startNode?.Left is not null)
                startNode = startNode.Left;
            return startNode;
        }

        internal static TreeNode<T> FindMax(TreeNode<T> startNode)
        {
            while (startNode?.Right is not null)
                startNode = startNode.Right;
            return startNode;
        }

        public T Min()
        {
            return Root is null ? default : FindMin(Root).Item;
        }
        public T Max()
        {
            return Root is null ? default : FindMax(Root).Item;
        }

        public TreeNode<T> Search(T item)
        {
            var temp = Root;
            while (temp is not null)
            {
                if (item.Equals(temp.Item)) break;
                if (item.CompareTo(temp.Item) < 0) temp = temp.Left;
                else temp = temp.Right;
            }
            return temp;
        }

        public bool Contains(T item)
        {
            return Search(item) is not null;
        }
        #endregion

        #region BFS DFS

        /// <summary>Root start.</summary>
        public IEnumerator<TreeNode<T>> BFS() => BFS(Root);
        public IEnumerator<TreeNode<T>> BFS(TreeNode<T> node)
        {
            Queue<TreeNode<T>> queue = new();
            TreeNode<T> temp;
            if (node is not null) queue.Enqueue(node);
            while (queue.Count > 0)
            {
                temp = queue.Dequeue();
                if (temp.Left is not null) queue.Enqueue(temp.Left);
                if (temp.Right is not null) queue.Enqueue(temp.Right);
                yield return temp;
            }
        }


        /// <summary>LNR. Root start.</summary>
        public IEnumerator<TreeNode<T>> DFS() => DFS(Root);

        /// <summary>LNR</summary>
        public IEnumerator<TreeNode<T>> DFS(TreeNode<T> node)
        {
            Stack<TreeNode<T>> stack = new();
            TreeNode<T> temp;
            if (node is not null) stack.Push(node);
            bool leftPush = true;
            while (stack.Count > 0)
            {
                temp = stack.Peek();
                if (leftPush)
                {
                    if (temp.Left is not null)
                        stack.Push(temp.Left);
                    else
                        leftPush = false;
                }
                else
                {
                    yield return stack.Pop();
                    if (temp.Right is not null)
                    {
                        stack.Push(temp.Right);
                        leftPush = true;
                    }
                }
            }
        }

        #endregion

        #region Enumerator, CopyTo

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public void CopyTo(TreeNode<T>[] array, int arrayIndex)
        {
            var eBFS = BFS();
            while (eBFS.MoveNext())
            {
                array[arrayIndex] = eBFS.Current;
                arrayIndex++;
            }
        }

        public TreeNode<T>[] ToArray()
        {
            TreeNode<T>[] array = new TreeNode<T>[Count];
            CopyTo(array, 0);
            return array;
        }

        public void CopyTo(Array array, int index) => CopyTo(array, index);

        ///<summary>BFS</summary>
        public IEnumerator<T> GetEnumerator()
        {
            var eBFS = BFS();
            while (eBFS.MoveNext())
                yield return eBFS.Current.Item;
        }

        ///<summary>BFS</summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Print
        public void Print(TreeNode<T> node)
        {
            if (node is null) throw new ArgumentNullException(ARGUMENT_NULL, new NullReferenceException("node"));

            var enBFS = BFS(node);
            List<string> list = new();
            int expListCount = 1;
            var startTreePos = Console.GetCursorPosition();
            int intervalSide = 2; //number of spaces between tree nodes
            int intervalHeight = 3; //number of lines - 1 between tree nodes, minimum 3

            int prevLine = 0;
            int prevMaxStr = 0;
            int prevPosTop = startTreePos.Top - 3;
            list.Add(node.ToString());

            while (enBFS.MoveNext())
            {
                if (list.Count == expListCount)
                {
                    int maxStr = 0;
                    int line;
                    int diffLine;

                    foreach (var item in list)
                        if (item is not null && item.Length > maxStr) maxStr = item.Length;

                    line = list.Count * (maxStr + intervalSide) - intervalSide;
                    diffLine = line - prevLine;
                    int offset = 0;

                    if (diffLine > 1)
                    {
                        offset = (diffLine + 1) / 2;
                        Console.MoveBufferArea(startTreePos.Left, startTreePos.Top, prevLine, prevPosTop + 4, startTreePos.Left + offset, startTreePos.Top);
                    }
                    else
                        maxStr += Math.Abs(diffLine) / list.Count;


                    int posTop = prevPosTop + intervalHeight;
                    int posLeft;
                    int prevPosLeft = startTreePos.Left;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] is null) continue;
                        posLeft = startTreePos.Left + i * (maxStr + intervalSide) + (maxStr - list[i].Length) / 2;
                        Console.SetCursorPosition(posLeft, posTop);
                        Console.Write(list[i]);

                        int parity = i & 1;
                        if (parity == 0)
                            prevPosLeft = startTreePos.Left + (i / 2) * (prevMaxStr + intervalSide) + maxStr / 2 + offset;

                        if (list.Count > 1)
                            PrintBranch(prevPosLeft + parity - 1, prevPosTop + 1, posLeft + parity, posTop - 1);
                    }

                    prevLine = line;
                    prevMaxStr = maxStr;
                    prevPosTop = posTop;
                    expListCount *= 2;
                    list.Clear();
                }

                list.Add(enBFS.Current.Left?.ToString());
                list.Add(enBFS.Current.Right?.ToString());
            }
        }

        private static void PrintBranch(int left, int top, int left2, int top2)
        {
            int direction = Math.Sign(left - left2);
            char down = direction > 0 ? '/' : '\\';
            int sideCount = Math.Abs(left - left2);
            //while (top <= top2 - 2)
            //{
            //    Console.SetCursorPosition(left, top);
            //    Console.Write(down);
            //    left -= direction;
            //    sideCount--;
            //    if (sideCount > 2)
            //    {
            //        Console.SetCursorPosition(left, top);
            //        Console.Write('_');
            //        left -= direction;
            //        sideCount--;
            //    }
            //    top++;
            //}
            Console.SetCursorPosition(left, top);
            Console.Write(down);
            left -= direction;
            sideCount--;
            while (sideCount > 1)
            {
                Console.SetCursorPosition(left, top);
                Console.Write('_');
                left -= direction;
                sideCount--;
            }
            Console.SetCursorPosition(left, top + 1);
            Console.Write(down);
        }
        #endregion
    }
}


