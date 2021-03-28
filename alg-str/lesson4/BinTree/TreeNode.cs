using System;

namespace BinTree
{
    public sealed class TreeNode<T>
    {
        public TreeNode<T> Parent { get; internal set; }
        public TreeNode<T> Left { get; internal set; }
        public TreeNode<T> Right { get; internal set; }
        public T Item { get; internal set; }
        /// <summary>
        ///Height starts counting from the Root = 0
        /// </summary>
        public int Height { get; internal set; }
        public override string ToString() => Item.ToString();

        internal TreeNode()
        {

        }

    }
}
