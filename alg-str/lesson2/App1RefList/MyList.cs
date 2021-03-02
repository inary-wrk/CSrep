using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("App1RefList.Tests")]
namespace GeekBrainsTests
{
    public class MyList : ILinkedList, IEnumerable<Node>
    {
        //exception messages
        internal const string EMPTY_LIST_NODE_NULL = "The node argument can only be null in an empty list.";
        internal const string NODE_NULL = "The node argument cannot be null.";
        internal const string LIST_NOT_CONTAIN_NODE = "The current list does not contain this node.";

        public Node FirstNode { get; private set; }
        public Node LastNode { get; private set; }


        public MyList(params int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                AddNode(values[i]);
            }
        }

        public MyList()
        {

        }


        public void AddNode(int value) => AddNodeAfter(LastNode, value);

        public void AddNodeAfter(Node node, int value)
        {
            if (node is null)
            {
                if (FirstNode is null)
                {
                    FirstNode = new Node { Value = value };
                    LastNode = FirstNode;
                    return;
                }
                else throw new ArgumentNullException(EMPTY_LIST_NODE_NULL, new NullReferenceException("node"));
            }

            var temp = node.NextNode;
            node.NextNode = new Node
            {
                Value = value,
                PrevNode = node,
                NextNode = temp
            };

            if (temp is null) LastNode = node.NextNode;
            else temp.PrevNode = node.NextNode;
        }

        public int GetCount()
        {
            int count = 0;
            var temp = FirstNode;
            while (!(temp is null))
            {
                count++;
                temp = temp.NextNode;
            }
            return count;
        }

        public Node FindNode(int searchValue)
        {
            var temp = FirstNode;

            while (!(temp is null))
            {
                if (temp?.Value == searchValue) return temp;
                temp = temp.NextNode;
            }
            return null;
        }

        public int FindNodeIndex(Node node)
        {
            if(node is null) throw new ArgumentNullException(NODE_NULL, new NullReferenceException("node"));
            int index = 0;
            var temp = FirstNode;
            while (!(temp is null))
            {
                if (temp == node) return index;
                temp = temp.NextNode;
                index++;
            }
            return -1;
        }

        public Node GetNode(int index)
        {
            int listCount = GetCount();
            if (index < 0 || index >= listCount) throw new IndexOutOfRangeException();

            Node temp = index < listCount / 2 ? FirstNode : LastNode;
            if (temp == FirstNode)
            {
                int i = 0;
                while (i != index)
                {
                    temp = temp.NextNode;
                    i++;
                }
            }
            else
            {
                int i = listCount - 1;
                while (index != i)
                {
                    temp = temp.PrevNode;
                    i--;
                }
            }

            return temp;
        }

        public void RemoveNode(Node node)
        {
            if (node is null) throw new ArgumentNullException(NODE_NULL, new NullReferenceException("node"));
            if (FindNodeIndex(node) == -1) throw new InvalidOperationException(LIST_NOT_CONTAIN_NODE);

            if (node.PrevNode is null && node.NextNode is null)
            {
                    FirstNode = null;
                    LastNode = null;
                    return;
            }

            if (node.PrevNode is null)
            {
                node.NextNode.PrevNode = null;
                FirstNode = node.NextNode;
                node.NextNode = null;
                return;
            }

            if (node.NextNode is null)
            {
                node.PrevNode.NextNode = null;
                LastNode = node.PrevNode;
                node.PrevNode = null;
                return;
            }

            node.PrevNode.NextNode = node.NextNode;
            node.NextNode.PrevNode = node.PrevNode;
            node.NextNode = null;
            node.PrevNode = null;
        }

        public void RemoveNode(int index) => RemoveNode(GetNode(index));

        public MyList CopyList()
        {
            var temp = FirstNode;
            MyList newList = new MyList();
            while (!(temp is null))
            {
                newList.AddNode(temp.Value);
                temp = temp.NextNode;
            }
            return newList;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new MyListEnumerator(this.FirstNode);
        }

        IEnumerator<Node> IEnumerable<Node>.GetEnumerator()
        {
            var temp = FirstNode;
            while (!(temp is null))
            {
                yield return temp;
                temp = temp.NextNode;
            }
        }
    }
}
