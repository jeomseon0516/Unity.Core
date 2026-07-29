using System;
using System.Linq;
using Jeomseon.Collections;
using NUnit.Framework;

namespace Jeomseon.Tests
{
    public sealed class DequeTests
    {
        [Test]
        public void AddAndDequeue_PreserveBothEndOrders()
        {
            Deque<int> deque = new();

            deque.AddLast(2);
            deque.AddFirst(1);
            deque.AddLast(3);

            Assert.That(deque.Count, Is.EqualTo(3));
            Assert.That(deque.PeekFirst(), Is.EqualTo(1));
            Assert.That(deque.PeekLast(), Is.EqualTo(3));
            Assert.That(deque.ToArray(), Is.EqualTo(new[] { 1, 2, 3 }));
            Assert.That(deque.DequeueFirst(), Is.EqualTo(1));
            Assert.That(deque.DequeueLast(), Is.EqualTo(3));
            Assert.That(deque.DequeueFirst(), Is.EqualTo(2));
            Assert.That(deque.Count, Is.Zero);
        }

        [Test]
        public void EmptyDeque_TryMethodsReturnFalse_AndThrowingMethodsThrow()
        {
            Deque<string> deque = new();

            Assert.That(deque.TryPeekFirst(out string first), Is.False);
            Assert.That(first, Is.Null);
            Assert.That(deque.TryPeekLast(out string last), Is.False);
            Assert.That(last, Is.Null);
            Assert.That(deque.TryDequeueFirst(out _), Is.False);
            Assert.That(deque.TryDequeueLast(out _), Is.False);
            Assert.Throws<InvalidOperationException>(() => deque.PeekFirst());
            Assert.Throws<InvalidOperationException>(() => deque.PeekLast());
            Assert.Throws<InvalidOperationException>(() => deque.DequeueFirst());
            Assert.Throws<InvalidOperationException>(() => deque.DequeueLast());
        }

        [Test]
        public void Clear_RemovesAllItems()
        {
            Deque<int> deque = new();
            deque.AddLast(10);
            deque.AddLast(20);

            deque.Clear();

            Assert.That(deque.Count, Is.Zero);
            Assert.That(deque.Contains(10), Is.False);
        }
    }
}
