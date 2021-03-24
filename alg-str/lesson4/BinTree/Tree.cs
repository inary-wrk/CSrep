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
        // TODO: сделать нормальный формат
        public void Print(TreeNode<T> startNode)
        {
            if (startNode is null) throw new ArgumentNullException(ARGUMENT_NULL, new NullReferenceException("node"));
            var enBFS = BFS(startNode);

            List<PrintNode> list = new();
            List<PrintNode> prevList = new();
            var startTreePos = Console.GetCursorPosition();
            startTreePos.Left++;
            int intervalSide = 2; //number of spaces between tree nodes
            int intervalHeight = 3; //number of lines - 1 between tree nodes, minimum 3
            int stop = 0;


            prevList.Add(new PrintNode(null, Child.Left));
            int maxLine = 0;
            int height = startNode.Height;
            int posTop = startTreePos.Top - intervalHeight;
            TreeNode<T> current = startNode;

            while (true)
            {
                //loop out
                if (enBFS.MoveNext())
                    current = enBFS.Current;
                else
                {
                    stop++;
                    height = -1;
                }


                if (current.Height != height)
                {
                    int maxStr = 0;
                    int line;
                    int diffLine;
                    posTop += intervalHeight;
                    var nodeStrArr = new string[list.Count];

                    for (int i = 0; i < list.Count; i++)
                    {
                        nodeStrArr[i] = list[i].node.Item.ToString();
                        if (nodeStrArr[i].Length > maxStr) maxStr = nodeStrArr[i].Length;
                    }

                    line = list.Count * (maxStr + intervalSide) - intervalSide;
                    diffLine = line - maxLine;

                    int offset = 1;
                    if (diffLine > 1)
                    {
                        offset = (diffLine + 2) / 2;
                        Console.MoveBufferArea(startTreePos.Left, startTreePos.Top,
                                              maxLine, posTop + 1,
                                              startTreePos.Left + offset, startTreePos.Top);
                    }
                    else
                        maxStr += Math.Abs(diffLine) / list.Count;


                    int posLeft;
                    int k = 0;
                    for (int j = 0; j < prevList.Count * 2;)
                    {
                        int parity = j & 1;
                        switch (prevList[j / 2].child)
                        {
                            case Child.NoChild:
                                j += 2;
                                continue;
                            case Child.Left when (parity == 0):
                                break;
                            case Child.Right when (parity == 1):
                                break;
                            case Child.Both:
                                break;
                            default:
                                j++;
                                continue;
                        }

                        posLeft = startTreePos.Left + j * (maxStr + intervalSide) + (maxStr - nodeStrArr[k].Length) / 2;
                        list[k].left = posLeft;

                        Console.SetCursorPosition(posLeft, posTop);
                        Console.Write(nodeStrArr[k]);
                        if (height != startNode.Height)
                            PrintBranch(prevList[j / 2].left + parity + offset, posTop - intervalHeight + 1, posLeft + parity, posTop - 1);

                        k++;
                        j++;
                        if (k >= list.Count) break;
                    }

                    if (line > maxLine) maxLine = line;
                    prevList = list;
                    list = new List<PrintNode>();

                    if (stop == 2) break;
                }


                var child = Child.NoChild;
                if (current.Left is not null)
                    child++;
                if (current.Right is not null)
                    child += 2;

                list.Add(new PrintNode(current, child));
                height = current.Height;
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

        enum Child : byte
        {
            NoChild,
            Left,
            Right,
            Both
        }

        private class PrintNode
        {
            internal TreeNode<T> node;
            internal int left;
            internal Child child;

            public PrintNode(TreeNode<T> node, Child child)
            {
                this.node = node;
                this.child = child;
            }
        }
        #endregion
    }
}


