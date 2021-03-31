using Xunit;
using BinTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinTree.Tests
{
    public class TreeTests
    {

        static int[] arrItems = { 40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34};
        static Tree<int> emptyTree = new();
        static Tree<int> oneNodeTree = new(6);
        static Tree<int> tree = new(arrItems);
        static Tree<int> treeBalanced = new(true, arrItems);
        /*
        {40}H:0─────────────────┐
        │                       │
        {37}H:1                 {41}H:1─┐
        │                               │
        {30}H:2─┐                       {60}H:2─────────────────┐
        │       │                       │                       │
        {-6}H:3 {33}H:3─┐               {50}H:3                 {70}H:3─┐
                │       │               │                               │
                {32}H:4 {34}H:4         {45}H:4─┐                       {80}H:4
                                        │       │
                                        {43}H:5 {46}H:5─┐
                                                        │
                                                        {47}H:6
        */

        public TreeTests()
        {
            emptyTree = new();
            oneNodeTree = new(6);
            tree = new(arrItems);
        }


        public static bool EqualTrees<T>(Tree<T> tree, Tree<T> tree2)
            where T : IEquatable<T>, IComparable<T>
        {
            if (tree.Count != tree2.Count) return false;

            var enTree = tree.BFS();
            var enTree2 = tree2.BFS();

            while (enTree.MoveNext() & enTree2.MoveNext())
            {
                if (!enTree.Current.Item.Equals(enTree2.Current.Item)) return false;

                //parent
                if (enTree.Current.Parent is not null && enTree2.Current.Parent is not null)
                {
                    if (!enTree.Current.Parent.Item.Equals(enTree2.Current.Parent.Item)) return false;
                }
                else
                {
                    if (enTree.Current.Parent != enTree2.Current.Parent) return false;
                }

                //left
                if (enTree.Current.Left is not null && enTree2.Current.Left is not null)
                {
                    if (!enTree.Current.Left.Item.Equals(enTree2.Current.Left.Item)) return false;
                }
                else
                {
                    if (enTree.Current.Left != enTree2.Current.Left) return false;
                }

                //right
                if (enTree.Current.Right is not null && enTree2.Current.Right is not null)
                {
                    if (!enTree.Current.Right.Item.Equals(enTree2.Current.Right.Item)) return false;
                }
                else
                {
                    if (enTree.Current.Right != enTree2.Current.Right) return false;
                }
            }

            if (enTree.MoveNext() != enTree2.MoveNext()) return false;

            return true;
        }

        public static bool BalanceCheck<T>(Tree<T> tree)
            where T : IEquatable<T>, IComparable<T>
        {
            var en = tree.DFS();
            while (en.MoveNext())
            {
                var bf = tree.BalanceFactor(en.Current);
                if (Math.Abs(bf) > 1) return false;
            }
            return true;
        }

        [Fact]
        public void AddTest()
        {
            Tree<int> tree = new();
            int[] items = new int[] { 40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34 };
            int[] expectedBFS = new int[] { 40, 37, 41, 30, 60, -6, 33, 50, 70, 32, 34, 45, 80, 43, 46, 47 };
            int[] expectedLNR = new int[] { -6, 30, 32, 33, 34, 37, 40, 41, 43, 45, 46, 47, 50, 60, 70, 80 };


            foreach (var item in items)
                tree.Add(item);

            int[] actualBFS = new int[tree.Count];
            int[] actualLNR = new int[tree.Count];

            var enBFS = tree.BFS();
            var enLNR = tree.DFS();

            int i = 0;
            bool linkToTree = true;
            while (enBFS.MoveNext() & enLNR.MoveNext())
            {
                if (enBFS.Current.TreeLink != tree) linkToTree = false;
                actualBFS[i] = enBFS.Current.Item;
                actualLNR[i] = enLNR.Current.Item;
                i++;
            }

            Assert.Equal(expectedBFS, actualBFS);
            Assert.Equal(expectedLNR, actualLNR);
            Assert.Equal(items.Length, tree.Count);
            Assert.True(linkToTree);
        }

        [Fact]
        public void AddTest_BalancedTree()
        {
            Tree<int> expectedBalancedTree = new(46, 60, 70, 80, 50, 47, 41, 45, 43, 33, 37, 34, 40, 30, -6, 32);
            Tree<int> treeBalanced = new(true);
            int[] items = new int[] { 40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34 };

            foreach (var item in items)
                treeBalanced.Add(item);

            Assert.True(BalanceCheck<int>(treeBalanced));
            Assert.All(items, item => Assert.Contains(item, treeBalanced));
            Assert.True(EqualTrees(treeBalanced, expectedBalancedTree));
        }

        [Fact()]
        public void AddTest_NullItem_Exception()
        {
            Tree<string> tree = new();
            string item = null;
            var expectedException = new System.ArgumentNullException(nameof(item), Tree<int>.ARGUMENT_NULL);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => tree.Add(item));
            Assert.Equal(expectedException.Message, ex.Message);
        }

        [Fact()]
        public void ClearTest()
        {
            Tree<int> tree = new(40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34);

            TreeNode<int>[] treeArr = new TreeNode<int>[tree.Count];
            tree.CopyTo(treeArr, 0);

            tree.Clear();

            foreach (var item in treeArr)
            {
                Assert.Null(item.Parent);
                Assert.Null(item.Left);
                Assert.Null(item.Right);
                Assert.Null(item.TreeLink);
            }

            Assert.Null(tree.Root);
            Assert.Empty(tree);
        }


        public static IEnumerable<object[]> RemoveSuccessTestCases =>
        new List<object[]>
        {   // tree, item to remove, expected tree

            //leaf
            //tree values {6}
            new object[] { oneNodeTree, 6, new Tree<int>() },
            //tree values {40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34}
            new object[] { null, 80, new Tree<int>(40, 41, 60, 70, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34) },
            new object[] { null, -6, new Tree<int>(40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, 33, 32, 34) },
            //one child
            new object[] { null, 41, new Tree<int>(40, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34) },
            new object[] { new Tree<int>(40, 41, 60, 70, 80, 50, 45, 46, 47, 43), 40, new Tree<int>(41, 60, 70, 80, 50, 45, 46, 47, 43) },
            //twochild
            // remove root successor root right
            new object[] { null, 40, new Tree<int>(41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34) },
            // remove any successor right with child
            new object[] { null, 45, new Tree<int>(40, 41, 60, 70, 80, 50, 46, 47, 43, 37, 30, -6, 33, 32, 34) },
            // remove any successor left leaf
            new object[] { null, 30, new Tree<int>(40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 32, -6, 33, 34) },
            // remove any successor left whith child
            new object[] { new Tree<int>(40, 42, 41, 60, 70, 80, 50, 45, 46, 47, 43, 44, 37, 30, -6, 33, 32, 34), 42, new Tree<int>(40, 43, 41, 60, 70, 80, 50, 45, 46, 47, 44, 37, 30, -6, 33, 32, 34) }
        };
        [Theory]
        [MemberData(nameof(RemoveSuccessTestCases))]
        public void RemoveSuccessTest(Tree<int> tree, int itemDelete, Tree<int> expectedTree)
        {
            if (tree is null) tree = TreeTests.tree;

            var nodeDelete = tree.Search(itemDelete);
            bool result = tree.Remove(nodeDelete);

            Assert.True(result);
            Assert.True(EqualTrees(tree, expectedTree), "Compare trees");

            Assert.Null(nodeDelete.Parent);
            Assert.Null(nodeDelete.Left);
            Assert.Null(nodeDelete.Right);
            Assert.Null(nodeDelete.TreeLink);
        }


        [Theory]
        [InlineData(45)]
        [InlineData(46)]
        [InlineData(70)]
        [InlineData(80)]
        public void RemoveTestBalanceCheck(int itemRemove)
        {
            Assert.True(treeBalanced.Remove(itemRemove));
            Assert.True(BalanceCheck(treeBalanced));
        }


        public static IEnumerable<object[]> RemoveTestNotSuccessCases =>
        new List<object[]>
        {   // tree, node to remove
            new object[] { tree, new TreeNode<int> () },
            new object[] { tree, null},
        };
        [Theory]
        [MemberData(nameof(RemoveTestNotSuccessCases))]
        public void RemoveTestNotSuccess(Tree<int> tree, TreeNode<int> nodeRemove)
        {
            Assert.False(tree.Remove(nodeRemove));
        }


        public static IEnumerable<object[]> SearchTestCases =>
        new List<object[]>
        {   // tree, target value, expected
            new object[] { emptyTree, 10, null },
            //tree values {6}
            new object[] { oneNodeTree, 6, oneNodeTree.Root },
            //tree values {40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34}
            new object[] { tree, 60, tree.Root.Right.Right}
        };
        [Theory]
        [MemberData(nameof(SearchTestCases))]
        public void SearchTest(Tree<int> tree, int targetValue, TreeNode<int> expected)
        {
            var actual = tree.Search(targetValue);

            Assert.Same(expected, actual);
        }

        public static IEnumerable<object[]> BFSTestCases => new List<object[]>
       {   // tree, start node, expected array of int
            new object[] { emptyTree, emptyTree.Root, new List<int>(0) },
            //tree values {6}
            new object[] { oneNodeTree, oneNodeTree.Root, new List<int> { 6 } },
            //tree values {40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34}
            new object[] { tree, tree.Root, new List<int> { 40, 37, 41, 30, 60, -6, 33, 50, 70, 32, 34, 45, 80, 43, 46, 47 } },
            new object[] { tree, tree.Root.Left.Left, new List<int> { 30, -6, 33, 32, 34 } }
       };
        [Theory]
        [MemberData(nameof(BFSTestCases))]
        public void BFSTest(Tree<int> tree, TreeNode<int> startNode, List<int> expected)
        {
            List<int> actual = new(tree.Count);

            var en = tree.BFS(startNode);
            while (en.MoveNext())
                actual.Add(en.Current.Item);

            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void BFSTest_StartNode_NotNull_NoReferenceToTree_Exception()
        {
            Tree<int> tree = new(10, 20);
            TreeNode<int> startNode = new();
            var en = tree.BFS(startNode);

            var ex = Assert.Throws<InvalidOperationException>(() => en.MoveNext());
            Assert.Equal(Tree<int>.TREE_NOT_CONTAIN_NODE, ex.Message);
        }


        public static IEnumerable<object[]> DFSTestCases => new List<object[]>
       {   // tree, start node, expected array of int
            new object[] { emptyTree, emptyTree.Root, new List<int>(0) },
            //tree values {6}
            new object[] { oneNodeTree, oneNodeTree.Root, new List<int> { 6 } },
            //tree values {40, 41, 60, 70, 80, 50, 45, 46, 47, 43, 37, 30, -6, 33, 32, 34}
            new object[] { tree, tree.Root, new List<int> { -6, 30, 32, 33, 34, 37, 40, 41, 43, 45, 46, 47, 50, 60, 70, 80 } },
            new object[] { tree, tree.Root.Left.Left, new List<int> { -6, 30, 32, 33, 34 } }
       };
        [Theory]
        [MemberData(nameof(DFSTestCases))]
        public void DFSTest(Tree<int> tree, TreeNode<int> startNode, List<int> expected)
        {
            List<int> actual = new(tree.Count);

            var en = tree.DFS(startNode);
            while (en.MoveNext())
                actual.Add(en.Current.Item);

            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void DFSTest_StartNode_NotNull_NoReferenceToTree_Exception()
        {
            Tree<int> tree = new(10, 20);
            TreeNode<int> startNode = new();
            var en = tree.DFS(startNode);

            var ex = Assert.Throws<InvalidOperationException>(() => en.MoveNext());
            Assert.Equal(Tree<int>.TREE_NOT_CONTAIN_NODE, ex.Message);
        }


        [Fact()]
        public void PrintTest_LNRIsNull_Exception()
        {
            IEnumerator<TreeNode<int>> LNR = null;
            var expectedException = new System.ArgumentNullException(nameof(LNR), Tree<int>.ARGUMENT_NULL);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Tree<int>.Print(LNR, 1, 1, true));
            Assert.Equal(expectedException.Message, ex.Message);
        }

        [Fact()]
        public void TreeCtorTest_CollectionNull_Exception()
        {
            ICollection<int> collection = null;
            var expectedException = new System.ArgumentNullException(nameof(collection), Tree<int>.ARGUMENT_NULL);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new Tree<int>(collection));
            Assert.Equal(expectedException.Message, ex.Message);
        }


    }
}