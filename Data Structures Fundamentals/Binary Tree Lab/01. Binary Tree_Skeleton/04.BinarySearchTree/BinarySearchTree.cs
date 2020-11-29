namespace _04.BinarySearchTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
            this.LeftChild = root.LeftChild;
            this.RightChild = root.RightChild;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            var currentNode = this.Root;
            while (currentNode != null)
            {
                var comparisonResult = currentNode.Value.CompareTo(element);
                if (comparisonResult == 0)
                {
                    return true;
                }
                else if (comparisonResult == 1)
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            var newElement = new Node<T>(element, null, null);
            if (this.Root == null)
            {
                this.Root = newElement;
            }
            else
            {
                var exists = this.Contains(element);
                if (!exists)
                {
                    this.AddToNode(newElement);
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var currentNode = this.Root;
            while (currentNode != null)
            {
                var comparisonResult = currentNode.Value.CompareTo(element);
                if (comparisonResult == 0)
                {
                    return new BinarySearchTree<T>(currentNode);
                }
                else if(comparisonResult == 1)
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }

            return null;
        }

        private void AddToNode(Node<T> newElement)
        {
            var currentNode = this.Root;
            while (true)
            {
                if (currentNode.Value.CompareTo(newElement.Value) == 1)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newElement;
                        break;
                    }
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = newElement;
                        break;
                    }
                    currentNode = currentNode.RightChild;
                }
            }
        }
    }
}
