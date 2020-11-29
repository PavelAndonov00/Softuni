namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
            : this()
        {
            this.Root = root;
            this.LeftChild = root.LeftChild;
            this.RightChild = root.RightChild;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.Root == null ? 0 : this.Root.Count;

        public bool Contains(T element)
        {
            var currentNode = this.Root;
            while (currentNode != null)
            {
                if (this.IsLess(element, currentNode.Value))
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (this.IsGreater(element, currentNode.Value))
                {
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            var newNode = new Node<T>(element, null, null);
            if (this.Root == null)
            {
                this.Root = newNode;
            }
            else
            {
                var exists = this.Contains(element);
                if (!exists)
                {
                    this.InsertRecur(this.Root, newNode);
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;
            if (current.Count == 1)
            {
                if (this.IsEqual(this.Root.Value, element))
                {
                    return new BinarySearchTree<T>(this.Root);
                }
            }
            else
            {
                while (current != null)
                {
                    if (this.IsLess(element, current.Value))
                    {
                        current = current.LeftChild;
                    }
                    else if (this.IsGreater(element, current.Value))
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        return new BinarySearchTree<T>(current);
                    }
                }
            }

            return null;
        }

        public void EachInOrder(Action<T> action)
        {
            if (this.Root != null)
            {
                this.EachInOrderRecur(this.Root, action);
            }
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            RangeRecursion(this.Root, result, lower, upper);
            return result;
        }

        public void DeleteMin()
        {
            this.ThrowIfEmpty();
            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                this.FindAndDeleteMin(this.Root);
            }
        }

        public void DeleteMax()
        {
            this.ThrowIfEmpty();
            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else
            {
                this.FindAndDeleteMax(this.Root);
            }
        }

        public int GetRank(T element)
        {
            var result = 0;
            GetRankRecursion(this.Root, element, ref result);
            return result;
        }

        private void GetRankRecursion(Node<T> current, T element, ref int count)
        {
            if (current == null)
            {
                return;
            }
            else if (this.IsLess(element, current.Value))
            {
                GetRankRecursion(current.LeftChild, element, ref count);
            }
            else if (this.IsGreater(element, current.Value))
            {
                if (this.IsEqual(current.Value, this.Root.Value))
                {
                    count++;
                    if (current.LeftChild != null)
                    {
                        count += current.LeftChild.Count;
                    }
                }
                else
                {
                    count += current.Count;
                }
                GetRankRecursion(current.RightChild, element, ref count);
            }
        }

        private void EachInOrderRecur(Node<T> currentNode, Action<T> action)
        {
            if (currentNode.LeftChild != null)
            {
                this.EachInOrderRecur(currentNode.LeftChild, action);
            }

            action.Invoke(currentNode.Value);

            if (currentNode.RightChild != null)
            {
                this.EachInOrderRecur(currentNode.RightChild, action);
            }
        }

        private void GetCount(Node<T> currentNode, ref int count)
        {
            if (currentNode != null)
            {
                count++;
                GetCount(currentNode.LeftChild, ref count);
                GetCount(currentNode.RightChild, ref count);
            }
        }

        private bool IsGreater(T element1, T element2)
        {
            return element1.CompareTo(element2) == 1;
        }

        private bool IsLess(T element1, T element2)
        {
            return element1.CompareTo(element2) == -1;
        }

        private bool IsEqual(T element1, T element2)
        {
            return element1.CompareTo(element2) == 0;
        }

        private void InsertRecur(Node<T> currentNode, Node<T> newNode)
        {
            if (currentNode != null)
            {
                currentNode.Count++;
                if (this.IsLess(newNode.Value, currentNode.Value))
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newNode;
                        return;
                    }
                    InsertRecur(currentNode.LeftChild, newNode);
                }
                else if (this.IsGreater(newNode.Value, currentNode.Value))
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = newNode;
                        return;
                    }
                    InsertRecur(currentNode.RightChild, newNode);
                }
            }
        }

        private void FindAndDeleteMin(Node<T> current)
        {
            if (current.LeftChild != null)
            {
                current.Count--;
                if (current.LeftChild.LeftChild == null && current.LeftChild.RightChild != null)
                {
                    current.LeftChild = current.LeftChild.RightChild;
                    current.Count--;
                    return;
                }

                if (current.LeftChild.LeftChild == null)
                {
                    current.LeftChild = null;
                    current.Count--;
                    return;
                }
                FindAndDeleteMin(current.LeftChild);
            }
        }

        private void ThrowIfEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("Empty tree");
            }
        }

        private void FindAndDeleteMax(Node<T> current)
        {
            if (current.RightChild != null)
            {
                current.Count--;
                if (current.RightChild.RightChild == null && current.RightChild.LeftChild != null)
                {
                    current.RightChild = current.RightChild.LeftChild;
                    return;
                }

                if (current.RightChild.RightChild == null)
                {
                    current.RightChild = null;
                    return;
                }

                FindAndDeleteMax(current.RightChild);
            }
        }

        private void RangeRecursion(Node<T> currentNode, List<T> result, T lower, T upper)
        {
            if (this.IsLess(currentNode.Value, upper)
                && this.IsGreater(currentNode.Value, lower))
            {
                result.Add(currentNode.Value);
            }
            else if(this.IsEqual(currentNode.Value, lower) || this.IsEqual(currentNode.Value, upper))
            {
                result.Add(currentNode.Value);
            }

            if (currentNode.LeftChild != null)
            {
                RangeRecursion(currentNode.LeftChild, result, lower, upper);
            }

            if (currentNode.RightChild != null)
            {
                RangeRecursion(currentNode.RightChild, result, lower, upper);
            }
        }
    }
}
