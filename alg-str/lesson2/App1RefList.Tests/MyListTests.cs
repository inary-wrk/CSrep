using NUnit.Framework;
using System;
using GeekBrainsTests;
using App1RefList;
using System.Collections.Generic;
using System.Text;

namespace GeekBrainsTests.Tests
{
    [TestFixture()]
    public class MyListTests
    {
        static MyList emptyList = new MyList();
        static MyList oneNodeList = new MyList(6);
        static MyList nodeList = new MyList(10, 12, 13, 16, 17);

        [TearDown]
        public void Init()
        {
            emptyList = new MyList();
            oneNodeList = new MyList(6);
            nodeList = new MyList(10, 12, 13, 16, 17);
        }


        static object[] AddNodeAfterTestCases =
        {   // list, node, value, expected array of list value
            new object[] { emptyList, emptyList.FirstNode, 123, new int[] {123} },
            //list values {6}
            new object[] { oneNodeList, oneNodeList.FirstNode, 8, new int[] {6, 8} },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, nodeList.LastNode.PrevNode.PrevNode, 20, new int[] {10, 12, 13, 20, 16, 17} }
        };

        [TestCaseSource(nameof(AddNodeAfterTestCases))]
        public void AddNodeAfterTest_AddNode_CreateNode(MyList list, Node node, int value, int[] expected)
        {
            list.AddNodeAfter(node, value);
            int listCount = list.GetCount();
            int[] firstStartActual = new int[listCount];
            int[] lastStartActual = new int[listCount];

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

            Assert.Multiple(() =>
            {
                Assert.NotNull(list.FirstNode);
                Assert.NotNull(list.LastNode);
                Assert.AreEqual(expected, firstStartActual);
                Assert.AreEqual(expected, lastStartActual);
            });
        }


        [Test()]
        public void AddNodeAfterTest_ListNotEmptyNodeParamNull_Exception()
        {
            var list = new MyList(12, 323, 14);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => list.AddNodeAfter(null, 3));
            Assert.That(ex.Message, Is.EqualTo(MyList.EMPTY_LIST_NODE_NULL));
        }


        static object[] GetCountTestCases =
        {   //list, expected count of items
            new object[] { emptyList, 0 },
            //list values {6}
            new object[] { oneNodeList, 1 },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, 5 }
        };

        [TestCaseSource(nameof(GetCountTestCases))]
        public void GetCountTest(MyList list, int expected)
        {
            int actual = list.GetCount();
            Assert.AreEqual(expected, actual);
        }


        static object[] FindNodeTestCases =
        {   //list, search value, expected
            new object[] { emptyList, 0, null },
            //list values {6}
            new object[] { oneNodeList, 6, oneNodeList.FirstNode },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, 12, nodeList.FirstNode.NextNode },
            new object[] { nodeList, 100, null }
        };

        [TestCaseSource(nameof(FindNodeTestCases))]
        public void FindNodeTest(MyList list, int searchValue, Node expected)
        {
            var actual = list.FindNode(searchValue);
            Assert.AreEqual(expected, actual);
        }


        static object[] GetNodeTestCases =
        {   //list, node index, expected node
            //list values {6}
            new object[] { oneNodeList, 0, oneNodeList.FirstNode },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, 3, nodeList.LastNode.PrevNode },
            new object[] { nodeList, 1, nodeList.FirstNode.NextNode },
        };

        [TestCaseSource(nameof(GetNodeTestCases))]
        public void GetNodeTest(MyList list, int index, Node expected)
        {
            var actual = list.GetNode(index);

            Assert.AreEqual(expected, actual);
        }


        static object[] GetNodeTestExceptionCases =
        {   //list, node index, expected node
            new object[] { emptyList, 0 },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, -10},
            new object[] { nodeList, 20},
        };

        [TestCaseSource(nameof(GetNodeTestExceptionCases))]
        public void GetNodeTest_IndexOutOfRange_Exception(MyList list, int index)
        {
            Assert.Throws<IndexOutOfRangeException>(() => list.GetNode(index));
        }


        static object[] RemoveNodeTestCases =
        {   // list, node, value, expected array of list value
            new object[] { emptyList, emptyList.FirstNode, 123, new int[] {123} },
            //list values {6}
            new object[] { oneNodeList, oneNodeList.FirstNode, 8, new int[] {6, 8} },
            //list values {10, 12, 13, 16, 17}
            new object[] { nodeList, nodeList.LastNode.PrevNode.PrevNode, 20, new int[] {10, 12, 13, 20, 16, 17} }
        };

        [TestCaseSource(nameof(RemoveNodeTestCases))]
        public void RemoveNodeTest()
        {

        }


        [Test()]
        public void RemoveNodeTest_NodeNull_Exception()
        {
            Node node = null;
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => nodeList.RemoveNode(null));
            Assert.That(ex.Message, Is.EqualTo(MyList.NODE_NULL));
        }

    }
}