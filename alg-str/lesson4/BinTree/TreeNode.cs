using System;

namespace BinTree
{
    public sealed class TreeNode<T>
    {
        public TreeNode<T> Parent { get; internal set; }
        public TreeNode<T> Left { get; internal set; }
        public TreeNode<T> Right { get; internal set; }
        public T item { get; set; }
    }
}
