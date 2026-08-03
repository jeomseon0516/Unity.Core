using System;
using System.Reflection;
using System.Text;
using Jeomseon.Text;
using NUnit.Framework;

namespace Jeomseon.Tests
{
    public sealed class StringBuilderPoolTests
    {
        [Test]
        public void ReleaseAndGet_ReusesClearedBuilder()
        {
            using StringBuilderPool pool = new(maxRetainedCount: 1);
            StringBuilder first = pool.Get();
            first.Append("temporary");

            pool.Release(first);
            StringBuilder second = pool.Get();

            Assert.That(second, Is.SameAs(first));
            Assert.That(second.Length, Is.Zero);
            pool.Release(second);
        }

        [Test]
        public void PooledObject_DisposeReturnsBuilderToSelectedPool()
        {
            using StringBuilderPool pool = new(maxRetainedCount: 1);
            StringBuilder scopedBuilder;

            using (pool.Get(out scopedBuilder))
            {
                scopedBuilder.Append("pooled");
            }

            StringBuilder returned = pool.Get();
            Assert.That(returned, Is.SameAs(scopedBuilder));
            Assert.That(returned.Length, Is.Zero);
            pool.Release(returned);
        }

        [Test]
        public void ReleaseSameBuilderTwice_Throws()
        {
            using StringBuilderPool pool = new(maxRetainedCount: 1);
            StringBuilder builder = pool.Get();
            pool.Release(builder);

            Assert.Throws<InvalidOperationException>(() => pool.Release(builder));
        }

        [Test]
        public void BuilderAboveMaxCapacity_IsNotRetained()
        {
            using StringBuilderPool pool = new(maxRetainedCount: 1, maxCapacity: 16);
            StringBuilder oversized = pool.Get();
            oversized.EnsureCapacity(32);

            pool.Release(oversized);

            Assert.That(pool.CountInactive, Is.Zero);
            Assert.That(pool.Get(), Is.Not.SameAs(oversized));
        }

        [Test]
        public void GetAfterDispose_Throws()
        {
            var pool = new StringBuilderPool();
            pool.Dispose();

            Assert.Throws<ObjectDisposedException>(() => pool.Get());
        }

        [Test]
        public void Pool_DoesNotDeclareFinalizer()
        {
            MethodInfo finalizer = typeof(StringBuilderPool).GetMethod(
                "Finalize",
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly);

            Assert.That(finalizer, Is.Null);
        }
    }
}
