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
        internal const string ARGUMENT_NEGATIVE = "The argument cannot be negative.";


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
        // TODO: balancing
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
        public static IEnumerator<TreeNode<T>> BFS(TreeNode<T> node)
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
        public static IEnumerator<TreeNode<T>> DFS(TreeNode<T> node)
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
        /// <summary>
        /// Draw a Tree. The enumerator must use LNR tree traversal.
        /// </summary>
        /// <param name="LNR">Binary tree traversal (left number right)</param>
        /// <param name="lineSpace">Line spacing.</param>
        /// <param name="nodeSpace">Interval between tree nodes.</param>
        public static void Print(IEnumerator<TreeNode<T>> LNR,  int lineSpace, int nodeSpace)
        {
            if (lineSpace < 0) throw new ArgumentOutOfRangeException("lineSpace", lineSpace, ARGUMENT_NEGATIVE);
            if (nodeSpace < 0) throw new ArgumentOutOfRangeException("nodeSpace", nodeSpace, ARGUMENT_NEGATIVE);

            lineSpace++;
            var startPos = Console.GetCursorPosition();
            int maxStr = 0;
            int indent = 0;
            int maxTop = 0;
            Stack<int> stackidnt = new();
            var temp = new TreeNode<T> { Height = -1 };
            int prevTop = 0;


            stackidnt.Push(0);
            while (LNR.MoveNext())
            {
                int top = startPos.Top + LNR.Current.Height * lineSpace;
                if (top > maxTop) maxTop = top;

                if (LNR.Current.Height > temp.Height && temp.Height != -1)
                {
                    int prevleft = stackidnt.Peek() + maxStr;

                    indent = indent + maxStr + nodeSpace + startPos.Left;
                    if (Console.BufferWidth <= indent) Console.BufferWidth = indent * 2;

                    stackidnt.Push(indent);

                    //print rigth branch
                    Console.SetCursorPosition(prevleft, prevTop);
                    while (prevleft < indent)
                    {
                        Console.Write('─');
                        prevleft++;
                    }

                    Console.Write('┐');

                    while (prevTop < top)
                    {
                        Console.SetCursorPosition(prevleft, prevTop + 1);
                        Console.Write('│');
                        prevTop++;
                    }

                    maxStr = 0;
                }
                else
                {

                    if (!ReferenceEquals(temp, LNR.Current.Left))
                        for (int i = 1; i < temp.Height - LNR.Current.Height; i++)
                        {
                            stackidnt.Pop();
                            prevTop -= lineSpace;
                        }

                    //print left branch
                    while (prevTop > top)
                    {
                        Console.SetCursorPosition(stackidnt.Peek(), prevTop - 1);
                        Console.Write('│');
                        prevTop--;
                    }
                }

                string str = LNR.Current.ToString();
                if (maxStr < str.Length) maxStr = str.Length;


                Console.SetCursorPosition(stackidnt.Peek(), top);
                Console.Write(str);
                temp = LNR.Current;
                prevTop = top;
            }

            Console.SetCursorPosition(0, maxTop + 1);
        }
        /// <summary>
        /// Draw a Tree.
        /// </summary>
        public void Print() => Print(DFS(Root), 2, 1);
        /// <summary>
        /// Draw a Tree.
        /// </summary>
        /// <param name="lineSpace">Line spacing.</param>
        /// <param name="nodeSpace">Interval between tree nodes.</param>
        public void Print(int lineSpace, int nodeSpace) => Print(DFS(Root), lineSpace, nodeSpace);
        /// <summary>
        /// Draw a subtree starting from this <paramref name="node"/>
        /// </summary>
        public void Print(TreeNode<T> node) => Print(DFS(node), 2, 1);
        /// <summary>
        /// Draw a subtree starting from this <paramref name="node"/>
        /// </summary>
        /// <param name="lineSpace">Line spacing.</param>
        /// <param name="nodeSpace">Interval between tree nodes.</param>
        public void Print(TreeNode<T> node, int lineSpace, int nodeSpace) => Print(DFS(node), lineSpace, nodeSpace);
        #endregion
    }
}
