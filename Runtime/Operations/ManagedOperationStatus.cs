namespace Jeomseon.Unity.Core.Operations
{
    /// <summary>Unity가 관리하는 작업의 현재 상태를 나타냅니다.</summary>
    public enum ManagedOperationStatus
    {
        /// <summary>작업이 실행 중입니다.</summary>
        Running,

        /// <summary>작업이 성공적으로 완료되었습니다.</summary>
        Completed,

        /// <summary>작업이 취소되었습니다.</summary>
        Canceled,

        /// <summary>작업이 예외로 종료되었습니다.</summary>
        Faulted
    }
}
