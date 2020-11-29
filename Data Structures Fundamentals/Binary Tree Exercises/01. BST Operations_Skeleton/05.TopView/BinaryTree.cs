namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
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

        public List<T> TopView()
        {
            var views = new SortedDictionary<int, KeyValuePair<T, int>>();
            var horizontalDistance = 0;
            var level = 0;
            var maxLevel = 0;
            PreOrderTraverse(this, views, horizontalDistance, level, maxLevel);

            var result = new List<T>();
            foreach (var kvp in views.Values)
            {
                result.Add(kvp.Key);
            }

            return result;
        }

        private void PreOrderTraverse(BinaryTree<T> current,
            SortedDictionary<int, KeyValuePair<T, int>> views,
            int horizontalDistance, int level, int maxLevel)
        {
            if (views.ContainsKey(horizontalDistance))
            {
                if (level < maxLevel)
                {
                    maxLevel = level;
                    views[horizontalDistance] = new KeyValuePair<T, int>(current.Value, level);
                }
            }
            else
            {
                views.Add(horizontalDistance, new KeyValuePair<T, int>(current.Value, level));
            }

            if (current.LeftChild != null)
            {
                PreOrderTraverse(current.LeftChild, views, horizontalDistance - 1, level + 1, maxLevel);
            }

            if (current.RightChild != null)
            {
                PreOrderTraverse(current.RightChild, views, horizontalDistance + 1, level + 1, maxLevel);
            }
        }
    }
}
