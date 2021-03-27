using System;
using System.Collections.Generic;

namespace BinTree
{
    using System.Collections;

    public class Tree<T> : ICollection<T>, ICollection
        where T : IComparable<T>, IEquatable<T>
    {
        private Object _syncRoot;

        public TreeNode<T> Root { get; private set; }
        public int Count { get; private set; }


        public bool IsReadOnly { get { return false; } }
        public bool IsSynchronized { get { return false; } }
        public bool Balanced { get; set; }


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

        public Tree(IEnumerable<T> collection, bool balanced = false)
        {
            Balanced = balanced;
            if (collection is null) throw new ArgumentNullException(ARGUMENT_NULL, new NullReferenceException("collection"));
            var en = collection.GetEnumerator();
            while (en.MoveNext())
                Add(en.Current);
        }

        public Tree(params T[] item) : this(false, item)
        {
        }

        public Tree(bool balanced, params T[] item)
        {
            Balanced = balanced;
            for (int i = 0; i < item.Length; i++)
                Add(item[i]);
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(ARGUMENT_NULL, new NullReferenceException("item"));
            //add root
            if (Root is null)
                Root = new TreeNode<T> { Item = item, Height = 0 };
            else
            {
                var temp = Root;
                while (true)
                {
                    int compare = item.CompareTo(temp.Item);
                    if (compare == 0) return;  // return if item alredy exist
                    if (compare < 0)
                    {
                        if (temp.Left is null)
                        {
                            temp.Left = new TreeNode<T> { Item = item, Parent = temp, Height = temp.Height + 1 };
                            break;
                        }
                        else temp = temp.Left;
                    }
                    else
                    {
                        if (temp.Right is null)
                        {
                            temp.Right = new TreeNode<T> { Item = item, Parent = temp, Height = temp.Height + 1 };
                            break;
                        }
                        else temp = temp.Right;
                    }
                }
                if (Balanced == true) Balance(temp);
            }
            Count++;
        }
        #endregion

        #region Balancing
        // TODO: fix change height

        internal static void Balance(TreeNode<T> node)
        {
            while (node is not null)
            {
                switch (BalanceFactor(node))
                {
                    case 2 when BalanceFactor(node.Left) == 1:
                        RightRotate(node);
                        break;
                    case 2 when BalanceFactor(node.Left) == -1:
                        LeftRotate(node.Left);
                        RightRotate(node);
                        break;
                    case -2 when BalanceFactor(node.Right) == -1:
                        LeftRotate(node);
                        break;
                    case -2 when BalanceFactor(node.Right) == 1:
                        RightRotate(node.Right);
                        LeftRotate(node);
                        break;
                    default:
                        break;
                }
                node = node.Parent;
            }
        }

        /// <summary>Left node cannot be null.</summary>
        internal static void RightRotate(TreeNode<T> node)
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

            //change height
            succesor.Height--;
            node.Height++;
            ChangeHeight(node.Right, 1);
            ChangeHeight(succesor.Left, -1);
        }

        /// <summary>Right node cannot be null.</summary>
        internal static void LeftRotate(TreeNode<T> node)
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

            //change height
            succesor.Height--;
            node.Height++;
            ChangeHeight(node.Left, 1);
            ChangeHeight(succesor.Right, -1);
        }

        internal static int BalanceFactor(TreeNode<T> node)
        {
            int left = node.Left is null ? node.Height : MaxHeight(node.Left);
            int right = node.Right is null ? node.Height : MaxHeight(node.Right);
            return left - right;
        }

        internal static int MaxHeight(TreeNode<T> node)
        {
            var enBFS = BFS(node);
            int maxHeight = 0;
            while (enBFS.MoveNext())
                if (enBFS.Current.Height > maxHeight) maxHeight = enBFS.Current.Height;
            return maxHeight;
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

                ChangeHeight(successor, -1);
                successor.Height = node.Height;
                successor.Parent = parent;
                Invalidate(node);
                return true;
            }

            // two child
            successor = FindMin(node.Right); // right successor
            ChangeHeight(successor, -1);
            //remove right successor
            parent = successor.Parent;
            if (successor.Right is not null) successor.Right.Parent = parent;
            parent.Left = successor.Right;

            //swap
            node.Item = successor.Item;
            Invalidate(successor);
            return true;
        }


        /// <summary>
        /// Change the <paramref name="Height"/> of the <paramref name="startNode"/> subtrees by <paramref name="changer"/>.
        /// </summary>
        internal static void ChangeHeight(TreeNode<T> startNode, SByte changer)
        {
            var enBFS = BFS(startNode);
            while (enBFS.MoveNext())
                enBFS.Current.Height += changer;
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

        public T[] ToArray()
        {
            T[] array = new T[Count];
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
        public static void Print(IEnumerator<TreeNode<T>> LNR, byte lineSpace, byte nodeSpace)
        {

            var startPos = Console.GetCursorPosition();
            lineSpace++;
            int maxStr = 0;
            int indent = 0;
            int maxTop = 0;
            Stack<int> stackidnt = new();
            var temp = new TreeNode<T> { Height = -1 };
            int prevTop = 0;
            int prevStrLenght = 0;

            stackidnt.Push(0);
            while (LNR.MoveNext())
            {
                int top = startPos.Top + LNR.Current.Height * lineSpace;
                if (top > maxTop) maxTop = top;

                if (LNR.Current.Height > temp.Height && temp.Height != -1)
                {
                    int prevleft = stackidnt.Peek() + prevStrLenght;
                    indent = indent + maxStr + nodeSpace + startPos.Left;
                    stackidnt.Push(indent);

                    if (Console.BufferWidth <= indent) Console.BufferWidth = indent + 10;

                    //print rigth branch
                    Console.ForegroundColor = ConsoleColor.Green;
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

                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    while (prevTop - 1 > top)
                    {
                        Console.SetCursorPosition(stackidnt.Peek(), prevTop - 1);
                        Console.Write('│');
                        prevTop--;
                    }
                    Console.ResetColor();
                }


                string str = "{" + LNR.Current.ToString() + "}";
                if (maxStr < str.Length) maxStr = str.Length;


                if (Console.BufferWidth <= indent + str.Length) Console.BufferWidth = indent + str.Length + 10;


                Console.SetCursorPosition(stackidnt.Peek(), top);
                Console.Write(str);
                temp = LNR.Current;
                prevTop = top;
                prevStrLenght = str.Length;
            }
            Console.SetCursorPosition(0, maxTop + 1);
        }
        /// <summary>
        /// Draw a Tree.
        /// </summary>
        /// <param name="lineSpace">Line spacing.</param>
        /// <param name="nodeSpace">Interval between tree nodes.</param>
        public void Print(byte lineSpace = 1, byte nodeSpace = 1) => Print(DFS(Root), lineSpace, nodeSpace);
        /// <summary>
        /// Draw a tree starting from this <paramref name="node"/>
        /// </summary>
        /// <param name="lineSpace">Line spacing.</param>
        /// <param name="nodeSpace">Interval between tree nodes.</param>
        public void Print(TreeNode<T> node, byte lineSpace = 1, byte nodeSpace = 1) => Print(DFS(node), lineSpace, nodeSpace);
        #endregion
    }
}
