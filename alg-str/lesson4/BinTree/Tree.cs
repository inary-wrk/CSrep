using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BinTree.Tests")]
namespace BinTree
{
    /// <summary>
    /// A binary tree. Can be a balanced AVL tree depending on the parameter <seealso cref="Balanced"/>
    /// </summary>
    public class Tree<T> : ICollection<T>, ICollection
        where T : IComparable<T>, IEquatable<T>
    {
        private Object _syncRoot;

        public TreeNode<T> Root { get; private set; }
        public int Count { get; private set; }


        public bool IsReadOnly { get { return false; } }
        public bool IsSynchronized { get { return false; } }
        /// <value>true - if the tree is a balanced AVL tree</value>
        public bool Balanced { get; private set; }
        //exception messages
        internal const string ARGUMENT_NULL = "The argument cannot be null.";
        internal const string TREE_NOT_CONTAIN_NODE = "The tree does not contain this node.";

        public object SyncRoot
        {
            get
            {
                if (_syncRoot is null)
                    System.Threading.Interlocked.CompareExchange<Object>(ref _syncRoot, new Object(), null);
                return _syncRoot;
            }
        }


        #region ctor, Add

        public Tree()
        {
        }

        public Tree(IEnumerable<T> collection, bool balanced = false)
        {
            if (collection is null) throw new ArgumentNullException(nameof(collection), ARGUMENT_NULL);
            Balanced = balanced;

            foreach (var item in collection)
                Add(item);
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
            if (item is null) throw new ArgumentNullException(nameof(item), ARGUMENT_NULL);
            //add root
            if (Root is null)
                Root = new TreeNode<T> { Item = item, Height = 0, TreeLink = this };
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
                            temp.Left = new TreeNode<T> { Item = item, Parent = temp, Height = temp.Height + 1, TreeLink = this };
                            break;
                        }
                        else temp = temp.Left;
                    }
                    else
                    {
                        if (temp.Right is null)
                        {
                            temp.Right = new TreeNode<T> { Item = item, Parent = temp, Height = temp.Height + 1, TreeLink = this };
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

        #region Utility
        /// <summary>
        /// Change the <paramref name="Height"/> of the <paramref name="startNode"/> and subtrees by <paramref name="changer"/>.
        /// </summary>
        internal void ChangeHeight(TreeNode<T> startNode, SByte changer)
        {
            if (startNode is null) return;

            var enBFS = BFS(startNode);
            while (enBFS.MoveNext())
                enBFS.Current.Height += changer;
        }

        internal void ValidateNode(TreeNode<T> node, string argName)
        {
            if (node is null) throw new ArgumentNullException(argName, ARGUMENT_NULL);

            if (node.TreeLink != this) throw new InvalidOperationException(TREE_NOT_CONTAIN_NODE);
        }


        #endregion

        #region Balancing
        public void DisableBalancing()
        {
            if (Balanced) Balanced = false;
        }

        internal void Balance(TreeNode<T> node)
        {
            while (node is not null)
            {
                switch (BalanceFactor(node))
                {

                    case 2 when BalanceFactor(node.Left) == -1:
                        LeftRotate(node.Left);
                        RightRotate(node);
                        break;
                    case 2:
                        RightRotate(node);
                        break;
                    case -2 when BalanceFactor(node.Right) == 1:
                        RightRotate(node.Right);
                        LeftRotate(node);
                        break;
                    case -2:
                        LeftRotate(node);
                        break;
                    default:
                        break;
                }
                node = node.Parent;
            }
        }

        /// <summary>Left node cannot be null.</summary>
        internal void RightRotate(TreeNode<T> node)
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
            else Root = succesor;

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
        internal void LeftRotate(TreeNode<T> node)
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
            else Root = succesor;

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

        internal int BalanceFactor(TreeNode<T> node)
        {
            int left = node.Left is null ? node.Height : MaxHeight(node.Left);
            int right = node.Right is null ? node.Height : MaxHeight(node.Right);
            return left - right;
        }

        internal int MaxHeight(TreeNode<T> node)
        {
            var enBFS = BFS(node);
            int maxHeight = 0;
            while (enBFS.MoveNext())
                if (enBFS.Current.Height > maxHeight) maxHeight = enBFS.Current.Height;
            return maxHeight;
        }
        #endregion

        #region Remove
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
            if (node is null || node.TreeLink != this) return false;
            Count--;

            TreeNode<T> successor;
            TreeNode<T> parent;

            //leaf
            if (node.Left is null && node.Right is null)
            {
                parent = node.Parent;
                if (parent is null) //remove root - leaf
                    Root = null;
                else
                {
                    if (node.Item.CompareTo(parent.Item) < 0)
                        parent.Left = null;
                    else
                        parent.Right = null;

                    node.Parent = null;
                    if (Balanced == true) Balance(parent);
                }
                node.TreeLink = null;
                return true;
            }


            // one child
            if (node.Left is null || node.Right is null)
            {
                successor = node.Left is null ? node.Right : node.Left;
                parent = node.Parent;

                ChangeHeight(successor, -1);
                successor.Parent = parent;
                Invalidate(node);

                if (parent is null) //remove root, one child
                {
                    Root = successor;
                    parent = successor;
                }
                else
                {
                    if (successor.Item.CompareTo(parent.Item) < 0)
                        parent.Left = successor;
                    else
                        parent.Right = successor;
                }

                if (Balanced == true)
                    Balance(parent);
                return true;
            }


            // two child
            successor = FindMin(node.Right); // right successor
            ChangeHeight(successor, -1);

            //remove right successor connection
            parent = successor.Parent;
            if (successor.Right is not null)
                successor.Right.Parent = parent;

            if (node.Right.Left is null)
            {
                node.Right = successor.Right;
                parent = successor;
            }
            else parent.Left = successor.Right;

            //swap node to successor
            var temp = node.Parent;
            if (temp is not null)
            {
                if (successor.Item.CompareTo(temp.Item) < 0)
                    temp.Left = successor;
                else temp.Right = successor;
            }
            else Root = successor;
            if (node.Right is not null) node.Right.Parent = successor;
            if (node.Left is not null) node.Left.Parent = successor;
            successor.Left = node.Left;
            successor.Right = node.Right;
            successor.Parent = node.Parent;
            successor.Height = node.Height;
            Invalidate(node);

            if (Balanced == true)
                Balance(parent);
            return true;
        }

        internal static void Invalidate(TreeNode<T> node)
        {
            if (node is null) return;
            node.Parent = null;
            node.Left = null;
            node.Right = null;
            node.TreeLink = null;
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

        /// <summary>
        /// Returns the minimum item in the tree.
        /// </summary>
        public T Min()
        {
            return Root is null ? default : FindMin(Root).Item;
        }
        /// <summary>
        /// Returns the maximum item in the tree.
        /// </summary>
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
        public IEnumerator<TreeNode<T>> BFS(TreeNode<T> startNode)
        {
            if (startNode is not null && startNode.TreeLink != this) throw new InvalidOperationException(TREE_NOT_CONTAIN_NODE);

            Queue<TreeNode<T>> queue = new();
            TreeNode<T> temp;
            if (startNode is not null) queue.Enqueue(startNode);
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
        public IEnumerator<TreeNode<T>> DFS(TreeNode<T> startNode)
        {
            if (startNode is not null && startNode.TreeLink != this) throw new InvalidOperationException(TREE_NOT_CONTAIN_NODE);

            Stack<TreeNode<T>> stack = new();
            TreeNode<T> temp;
            if (startNode is not null) stack.Push(startNode);
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
        /// <exception cref="ArgumentNullException">Throw when <paramref name="LNR"/> is null.</exception>
        public static void Print(IEnumerator<TreeNode<T>> LNR, byte lineSpace, byte nodeSpace, bool showHeight)
        {
            if (LNR is null) throw new ArgumentNullException(nameof(LNR), ARGUMENT_NULL);

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
                if (showHeight) str = str + "H:" + LNR.Current.Height;

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
        public void Print(byte lineSpace = 1, byte nodeSpace = 1, bool showHeight = false) => Print(DFS(Root), lineSpace, nodeSpace, showHeight);
        /// <summary>
        /// Draw a tree starting from this <paramref name="startNode"/>
        /// </summary>
        /// <param name="lineSpace">Line spacing.</param>
        /// <param name="nodeSpace">Interval between tree nodes.</param>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="startNode"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Throw when tree doesn't contain <paramref name="startNode"/>.</exception>
        public void Print(TreeNode<T> startNode, byte lineSpace = 1, byte nodeSpace = 1, bool showHeight = false)
        {
            ValidateNode(startNode, nameof(startNode));
            Print(DFS(startNode), lineSpace, nodeSpace, showHeight);
        }

        #endregion
    }
}
