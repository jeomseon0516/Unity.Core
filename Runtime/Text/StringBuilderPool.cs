using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Pool;

namespace Jeomseon.Text
{
    /// <summary>
    /// Provides a thread-safe StringBuilder pool compatible with Unity's IObjectPool contract.
    /// Unity IObjectPool 계약과 호환되는 스레드 안전 StringBuilder 풀을 제공합니다.
    /// </summary>
    public sealed class StringBuilderPool :
        IObjectPool<StringBuilder>,
        IDisposable
    {
        /// <summary>
        /// Gets the shared process-wide pool.
        /// 프로세스 전체에서 공유하는 풀을 가져옵니다.
        /// </summary>
        public static StringBuilderPool Shared { get; } = new();
        private readonly object _syncRoot = new();
        private readonly Stack<StringBuilder> _pool;
        private readonly HashSet<StringBuilder> _inactiveBuilders = new();

        private bool _disposed;
        /// <summary>
        /// Gets the maximum number of builders retained while inactive.
        /// 비활성 상태로 보관할 Builder의 최대 개수를 가져옵니다.
        /// </summary>
        public int MaxRetainedCount { get; }

        /// <summary>
        /// Gets the largest builder capacity accepted on release.
        /// 반환 시 허용할 Builder의 최대 용량을 가져옵니다.
        /// </summary>
        public int MaxCapacity { get; }

        /// <summary>
        /// Gets the number of builders currently retained by the pool.
        /// 현재 풀에 보관 중인 Builder 개수를 가져옵니다.
        /// </summary>
        public int CountInactive
        {
            get
            {
                lock (_syncRoot)
                {
                    return _pool.Count;
                }
            }
        }

        /// <summary>
        /// Gets a StringBuilder from the pool.
        /// 풀에서 StringBuilder를 가져옵니다.
        /// </summary>
        public StringBuilder Get()
        {
            lock (_syncRoot)
            {
                ThrowIfDisposed();
                if (_pool.TryPop(out StringBuilder builder))
                {
                    _inactiveBuilders.Remove(builder);
                    return builder;
                }

                return new StringBuilder();
            }
        }

        /// <summary>
        /// Gets a builder and a value-type token that returns it when disposed.
        /// Builder와 Dispose 시 이를 반환하는 값 형식 토큰을 가져옵니다.
        /// </summary>
        public PooledObject<StringBuilder> Get(out StringBuilder builder)
        {
            return new PooledObject<StringBuilder>(builder = Get(), this);
        }

        /// <summary>
        /// Clears and returns a builder, discarding it when its capacity is too large.
        /// Builder를 비우고 반환하며 용량이 지나치게 크면 폐기합니다.
        /// </summary>
        public void Release(StringBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            lock (_syncRoot)
            {
                if (_disposed || builder.Capacity > MaxCapacity)
                {
                    return;
                }

                if (!_inactiveBuilders.Add(builder))
                {
                    throw new InvalidOperationException(
                        "The StringBuilder has already been released to this pool.");
                }

                builder.Clear();
                if (_pool.Count < MaxRetainedCount)
                {
                    _pool.Push(builder);
                    return;
                }

                _inactiveBuilders.Remove(builder);
            }
        }

        /// <summary>
        /// Creates a StringBuilder pool with retention and capacity limits.
        /// 보관 개수 및 용량 제한을 지정하여 StringBuilder 풀을 생성합니다.
        /// </summary>
        public StringBuilderPool(
            int maxRetainedCount = 100,
            int maxCapacity = 1024)
        {
            if (maxRetainedCount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxRetainedCount),
                    "The maximum retained count must be greater than zero.");
            }

            if (maxCapacity < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxCapacity),
                    "The maximum capacity cannot be negative.");
            }

            MaxRetainedCount = maxRetainedCount;
            MaxCapacity = maxCapacity;
            _pool = new Stack<StringBuilder>(maxRetainedCount);
        }

        /// <summary>
        /// Removes every inactive builder retained by the pool.
        /// 풀에 보관 중인 모든 비활성 Builder를 제거합니다.
        /// </summary>
        public void Clear()
        {
            lock (_syncRoot)
            {
                _pool.Clear();
                _inactiveBuilders.Clear();
            }
        }

        /// <summary>
        /// Releases all retained references and prevents further rentals.
        /// 보관 중인 모든 참조를 해제하고 이후 대여를 금지합니다.
        /// </summary>
        public void Dispose()
        {
            lock (_syncRoot)
            {
                if (_disposed) return;

                _disposed = true;
                _pool.Clear();
                _inactiveBuilders.Clear();
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(StringBuilderPool));
            }
        }
    }
}
