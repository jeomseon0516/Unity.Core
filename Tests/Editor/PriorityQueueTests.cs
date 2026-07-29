using System;
using System.Collections.Generic;
using Jeomseon.Collections;
using NUnit.Framework;

namespace Jeomseon.Tests
{
    public sealed class PriorityQueueTests
    {
        [Test]
        public void DefaultComparer_PopsLargestValueFirst()
        {
            PriorityQueue<int> queue = new();
            queue.Push(3);
            queue.Push(1);
            queue.Push(5);
            queue.Push(2);

            Assert.That(queue.Peek(), Is.EqualTo(5));
            Assert.That(queue.Pop(), Is.EqualTo(5));
            Assert.That(queue.Pop(), Is.EqualTo(3));
            Assert.That(queue.Pop(), Is.EqualTo(2));
            Assert.That(queue.Pop(), Is.EqualTo(1));
            Assert.That(queue.Count, Is.Zero);
        }

        [Test]
        public void ReverseComparer_PopsSmallestValueFirst()
        {
            PriorityQueue<int> queue = new(Comparer<int>.Create((left, right) => right.CompareTo(left)));
            queue.Push(3);
            queue.Push(1);
            queue.Push(2);

            Assert.That(queue.Pop(), Is.EqualTo(1));
            Assert.That(queue.Pop(), Is.EqualTo(2));
            Assert.That(queue.Pop(), Is.EqualTo(3));
        }

        [Test]
        public void EmptyQueue_PeekAndPopThrow()
        {
            PriorityQueue<int> queue = new();

            Assert.Throws<InvalidOperationException>(() => queue.Peek());
            Assert.Throws<InvalidOperationException>(() => queue.Pop());
        }

        [Test]
        public void Clear_RemovesAllItems()
        {
            PriorityQueue<int> queue = new();
            queue.Push(1);
            queue.Push(2);

            queue.Clear();

            Assert.That(queue.Count, Is.Zero);
        }
    }
}
