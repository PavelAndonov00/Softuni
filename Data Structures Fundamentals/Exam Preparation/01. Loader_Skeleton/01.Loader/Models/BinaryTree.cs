namespace _01.Loader.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }
        public BinaryTree<T> LeftChild { get; set; }
        public BinaryTree<T> RightChild { get; set; }
    }
}
