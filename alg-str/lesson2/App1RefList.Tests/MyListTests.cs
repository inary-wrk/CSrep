using Xunit;
using GeekBrainsTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBrainsTests.Tests
{
    public class MyListTests
    {
        static MyList emptyList = new MyList();
        static MyList oneNodeList = new MyList(6);
        static MyList nodeList = new MyList(10, 12, 13, 16, 17);


        public MyListTests()
        {
            emptyList = new MyList();
            oneNodeList = new MyList(6);
            nodeList = new MyList(10, 12, 13, 16, 17);
        }


        public static IEnumerable<object[]> AddNodeAfterTestCases =>
        new List<object[]>
        {   // list, node, value, expected array of list value
            new object[] { emptyList, emptyList.FirstNode, 123, new int[] {123} },
            //list values {6}
            new object[] { oneNodeList, oneNodeList.FirstNode, 8, new int[] {6, 8} },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, nodeList.LastNode.PrevNode.PrevNode, 20, new int[] {10, 12, 13, 20, 16, 17} }
        };

        [Theory]
        [MemberData(nameof(AddNodeAfterTestCases))]
        public void AddNodeAfterTest_AddNode_CreateNode(MyList list, Node node, int value, int[] expected)
        {
            list.AddNodeAfter(node, value);
            int listCount = list.GetCount();
            var firstStartActual = new int[listCount];
            var lastStartActual = new int[listCount];

            var temp = list.FirstNode;
            int i = 0;
            while (!(temp is null))
            {
                firstStartActual[i] = temp.Value;
                temp = temp.NextNode;
                i++;
            }

            temp = list.LastNode;
            i = listCount - 1;
            while (!(temp is null))
            {
                lastStartActual[i] = temp.Value;
                temp = temp.PrevNode;
                i--;
            }

            Assert.NotNull(list.FirstNode);
            Assert.NotNull(list.LastNode);
            Assert.Equal(expected, firstStartActual);
            Assert.Equal(expected, lastStartActual);
        }


        [Fact()]
        public void AddNodeAfterTest_ListNotEmptyNodeParamNull_Exception()
        {
            var list = new MyList(12, 323, 14);
            Node node = null;

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => list.AddNodeAfter(node, 3));
            Assert.Equal(MyList.EMPTY_LIST_NODE_NULL, ex.Message);
        }


        public static IEnumerable<object[]> GetCountTestCases =>
        new List<object[]>
        {   //list, expected count of items
            new object[] { emptyList, 0 },
            //list values {6}
            new object[] { oneNodeList, 1 },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, 5 }
        };

        [Theory]
        [MemberData(nameof(GetCountTestCases))]
        public void GetCountTest(MyList list, int expected)
        {
            int actual = list.GetCount();
            Assert.Equal(expected, actual);
        }


        public static IEnumerable<object[]> FindNodeTestCases =>
        new List<object[]>
        {   //list, search value, expected
            new object[] { emptyList, 0, null },
            //list values {6}
            new object[] { oneNodeList, 6, oneNodeList.FirstNode },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, 12, nodeList.FirstNode.NextNode },
            new object[] { nodeList, 100, null }
        };

        [Theory]
        [MemberData(nameof(FindNodeTestCases))]
        public void FindNodeTest(MyList list, int searchValue, Node expected)
        {
            var actual = list.FindNode(searchValue);
            Assert.Equal(expected, actual);
        }


        public static IEnumerable<object[]> FindNodeIndexTestCases =>
        new List<object[]>
        {   //list, search node, expected index
            //list values {6}
            new object[] { oneNodeList, oneNodeList.FirstNode, 0 },
            new object[] { oneNodeList, new Node {Value = 65 }, -1 },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, nodeList.FirstNode.NextNode, 1 },
            new object[] { nodeList, nodeList.LastNode, 4 },
            new object[] { nodeList, oneNodeList.FirstNode, -1 }
        };

        [Theory]
        [MemberData(nameof(FindNodeIndexTestCases))]
        public void FindNodeIndexTest(MyList list, Node node, int expected)
        {
            int actual = list.FindNodeIndex(node);
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void FindNodeIndexTest_NodeNull_Exception()
        {
            var tempList = new MyList(0);
            Node node = null;

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => tempList.RemoveNode(node));
            Assert.Equal(MyList.NODE_NULL, ex.Message);
        }


        public static IEnumerable<object[]> GetNodeTestCases =>
        new List<object[]>
        {   //list, node index, expected node
            new object[] { emptyList, 0, null },
            //list values {6}
            new object[] { oneNodeList, 0, oneNodeList.FirstNode },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, 3, nodeList.LastNode.PrevNode },
            new object[] { nodeList, 1, nodeList.FirstNode.NextNode },
        };

        [Theory]
        [MemberData(nameof(GetNodeTestCases))]
        public void GetNodeTest(MyList list, int index, Node expected)
        {
            var actual = list.GetNode(index);

            Assert.Equal(expected, actual);
        }


        public static IEnumerable<object[]> GetNodeTestExceptionCases =>
        new List<object[]>
        {   //list, node index, expected node
            //list values {6}
            new object[] { oneNodeList, 1},
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, -10},
            new object[] { nodeList, 20},
        };

        [Theory]
        [MemberData(nameof(GetNodeTestExceptionCases))]
        public void GetNodeTest_IndexOutOfRange_Exception(MyList list, int index)
        {
            IndexOutOfRangeException ex = Assert.Throws<IndexOutOfRangeException>(() => list.GetNode(index));
            Assert.Equal(MyList.INDEX_OUT_OF_LIST_RANGE, ex.Message);
        }


        public static IEnumerable<object[]> RemoveNodeTestCases =>
        new List<object[]>
        {   // list, node, expected array of list value
            //list values {6}
            new object[] { oneNodeList, oneNodeList.FirstNode, new int[0] },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, nodeList.FirstNode.NextNode, new int[] {10, 13, 16, 17} }
        };

        [Theory]
        [MemberData(nameof(RemoveNodeTestCases))]
        public void RemoveNodeTest(MyList list, Node node, int[] expected)
        {
            list.RemoveNode(node);
            int listCount = list.GetCount();
            var firstStartActual = new int[listCount];
            var lastStartActual = new int[listCount];

            var temp = list.FirstNode;
            int i = 0;
            while (!(temp is null))
            {
                firstStartActual[i] = temp.Value;
                temp = temp.NextNode;
                i++;
            }

            temp = list.LastNode;
            i = listCount - 1;
            while (!(temp is null))
            {
                lastStartActual[i] = temp.Value;
                temp = temp.PrevNode;
                i--;
            }
            Assert.Equal(expected, firstStartActual);
            Assert.Equal(expected, lastStartActual);
        }


        [Fact]
        public void RemoveNodeTest_NodeNull_Exception()
        {
            var tempList = new MyList(0);
            Node node = null;

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => tempList.RemoveNode(node));
            Assert.Equal(MyList.NODE_NULL, ex.Message);
        }


        [Fact]
        public void RemoveNodeTest_ListNotContainNode_Exception()
        {
            var tempList = new MyList(0);
            Node node = new Node { Value = 0 };

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => tempList.RemoveNode(node));
            Assert.Equal(MyList.LIST_NOT_CONTAIN_NODE, ex.Message);
        }


        [Fact]
        public void CopyListTest_CopyList_CopiedValueEqualBase()
        {
            var list = new MyList(10, 16, 18, 24, 26);
            var copiedList = list.CopyList();
            bool successful = true;

            if (list.GetCount() == copiedList.GetCount())
            {
                var tempNodeList = list.FirstNode;
                var tempCopiedList = copiedList.FirstNode;
                while (!(tempNodeList is null && tempCopiedList is null))
                {
                    if (tempNodeList.Value != tempCopiedList.Value)
                    { successful = false; break; }

                    if (tempNodeList.PrevNode is null || tempCopiedList.PrevNode is null)
                    {
                        if (tempNodeList.PrevNode != tempCopiedList.PrevNode) { successful = false; break; }
                    }
                    else
                    {
                        if (tempNodeList.PrevNode == tempCopiedList.PrevNode) { successful = false; break; }
                    }

                    if (tempNodeList.NextNode is null || tempCopiedList.NextNode is null)
                    {
                        if (tempNodeList.NextNode != tempCopiedList.NextNode) { successful = false; break; }
                    }
                    else
                    {
                        if (tempNodeList.NextNode == tempCopiedList.NextNode) { successful = false; break; }
                    }


                    tempNodeList = tempNodeList.NextNode;
                    tempCopiedList = tempCopiedList.NextNode;
                }
            }
            else { successful = false; }
            Assert.True(successful);
        }

    }
}
