namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            if(leftChild != null)
            {
                this.LeftChild = leftChild;
                this.LeftChild.Parent = this;
            }
            
            if(rightChild != null)
            {
                this.RightChild = rightChild;
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var firstNode = this.FindNode(this, first);

            var currentParent = firstNode.Parent;
            while (currentParent != null)
            {
                if (this.FindNode(currentParent, second) != null)
                {
                    return currentParent.Value;
                }
                else
                {
                    currentParent = currentParent.Parent;
                }
            }

            return default;
        }

        private BinaryTree<T> FindNode(BinaryTree<T> current, T wanted)
        {
            var queue = new Queue<BinaryTree<T>>();
            while (current != null)
            {
                if(this.IsEqual(current.Value, wanted))
                {
                    return current;
                }

                if(current.LeftChild != null)
                {
                    queue.Enqueue(current.LeftChild);
                }

                if(current.RightChild != null)
                {
                    queue.Enqueue(current.RightChild);
                }

                current = queue.Dequeue();
            }

            return null;
        }

        private bool IsEqual(T v1, T v2)
        {
            return v1.CompareTo(v2) == 0;
        }
    }
}
