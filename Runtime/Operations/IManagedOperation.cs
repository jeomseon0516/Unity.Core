using System;

namespace Jeomseon.Unity.Operations
{
    /// <summary>
    /// Unity 수명에 따라 취소할 수 있고 종료 상태를 관찰할 수 있는 작업을 나타냅니다.
    /// </summary>
    public interface IManagedOperation
    {
        /// <summary>현재 작업 상태를 가져옵니다.</summary>
        ManagedOperationStatus Status { get; }

        /// <summary>작업이 예외로 종료된 경우 해당 예외를 가져옵니다.</summary>
        Exception Exception { get; }

        /// <summary>작업이 완료, 취소 또는 예외로 종료되었는지 나타냅니다.</summary>
        bool IsCompleted { get; }

        /// <summary>작업이 종료될 때 발생합니다.</summary>
        event Action<IManagedOperation> Completed;

        /// <summary>실행 중인 작업의 취소를 요청합니다.</summary>
        void Cancel();
    }
}
