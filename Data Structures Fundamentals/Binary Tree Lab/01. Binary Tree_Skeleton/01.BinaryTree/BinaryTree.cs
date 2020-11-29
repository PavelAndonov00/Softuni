namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder sb = new StringBuilder();
            AsIndentedPreOrderDfs(this, indent, sb);

            return sb.ToString().TrimStart();
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.InOrder());
            }
            result.Add(this);
            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.InOrder());
            }

            return result;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.PostOrder());
            }
            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.PostOrder());
            }
            result.Add(this);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            result.Add(this);
            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.PreOrder());
            }
            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.PreOrder());
            }

            return result;
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }
            action.Invoke(this.Value);
            if (this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }

        private void AsIndentedPreOrderDfs(IAbstractBinaryTree<T> binaryTree, int indent, StringBuilder sb)
        {
            sb.AppendLine(new string(' ', indent) + binaryTree.Value);
            if (binaryTree.LeftChild != null)
            {
                this.AsIndentedPreOrderDfs(binaryTree.LeftChild, indent + 2, sb);
            }

            if (binaryTree.RightChild != null)
            {
                this.AsIndentedPreOrderDfs(binaryTree.RightChild, indent + 2, sb);
            }
        }
    }
}
