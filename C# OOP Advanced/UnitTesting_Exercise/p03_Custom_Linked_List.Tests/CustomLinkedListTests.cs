namespace p03_Custom_Linked_List.Tests
{
    using CustomLinkedList;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class CustomLinkedListTests
    {
        private DynamicList<int> dynamicList;

        [SetUp]
        public void Initialization()
        {
            dynamicList = new DynamicList<int>();
        }

        [Test]
        public void AddMethodShouldAddCorrectlys()
        {
            dynamicList.Add(3);
            dynamicList.Add(4);
            dynamicList.Add(5);
            var expected = new int[] { 3, 4, 5 };
            var actual = new int[] { dynamicList[0], dynamicList[1], dynamicList[2] };

            Assert.That(expected, Is.EqualTo(actual));
        }

        [TestCase(-2)]
        [TestCase(3)]
        [TestCase(4)]
        public void RemoveMethodShouldThrowIfInvalidIndexIsPassed(int index)
        {
            dynamicList.Add(3);
            dynamicList.Add(4);
            dynamicList.Add(5);

            Assert.Throws<ArgumentOutOfRangeException>(() => dynamicList.RemoveAt(index));
        }

        [Test]
        public void RemoveAtMethodShouldRemoveCorrectly()
        {
            dynamicList.Add(3);
            dynamicList.Add(4);
            dynamicList.Add(5);
            dynamicList.RemoveAt(1);
            dynamicList.RemoveAt(1);
            var expected = new int[] { 3 };
            var actual = new int[] { dynamicList[0] };

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void RemoveMethodShouldReturnCorrectIndexIfElementIsRemoved()
        {
            dynamicList.Add(3);
            dynamicList.Add(4);
            dynamicList.Add(5);
            int removedIndex = dynamicList.Remove(3);

            Assert.That(removedIndex, Is.EqualTo(0));
        }

        [Test]
        public void RemoveMethodShouldReturnNegativeIfElementIsNotFound()
        {
            dynamicList.Add(3);
            dynamicList.Add(4);
            dynamicList.Add(5);
            int removedIndex = dynamicList.Remove(10);

            Assert.That(removedIndex, Is.EqualTo(-1));
        }

        [Test]
        public void RemoveMethodShouldRemoveCorrectly()
        {
            dynamicList.Add(3);
            dynamicList.Add(4);
            dynamicList.Add(5);
            dynamicList.Remove(3);
            dynamicList.Remove(5);
            var expected = new int[] { 4 };
            var actual = new int[] { dynamicList[0] };

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void IndexOfMethodShouldReturnCorrectIndexIfElementIsPresent()
        {
            dynamicList.Add(3);
            dynamicList.Add(4);
            dynamicList.Add(5);
            
            var expected = 1;
            var actual = dynamicList.IndexOf(4);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void IndexOfMethodShouldReturnNegativeIndexIfElementIsNotFound()
        {
            dynamicList.Add(555);
            dynamicList.Add(666);
            dynamicList.Add(777);

            var expected = -1;
            var actual = dynamicList.IndexOf(111);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void ContainsMethodShouldReturnTrueIfElementIsPresent()
        {
            dynamicList.Add(11);
            dynamicList.Add(22);
            dynamicList.Add(33);

            var expected = true;
            var actual = dynamicList.Contains(22);

            Assert.That(expected, Is.EqualTo(expected));
        }

        [Test]
        public void ContainsMethodShouldReturnFalseIfElementIsNotFound()
        {
            dynamicList.Add(44);
            dynamicList.Add(55);
            dynamicList.Add(66);

            var expected = false;
            var actual = dynamicList.Contains(444);

            Assert.That(expected, Is.EqualTo(expected));
        }

        [Test]
        public void CountPropertyShouldReturnCorrectCountOfArray()
        {
            dynamicList.Add(1);
            dynamicList.Add(22);
            dynamicList.Add(333);
            dynamicList.Add(4444);
            dynamicList.Add(55555);
            dynamicList.Add(666666);

            var expected = 6;
            var actual = dynamicList.Count;

            Assert.That(expected, Is.EqualTo(expected));
        }
    }
}
