# Core 어셈블리 경계

## `Jeomseon.Core` 외부 패키지

Unity 엔진 참조가 없는 별도 저장소의 `netstandard2.1` CLR 어셈블리입니다.

- 원형 배열 `Deque`와 우선순위 분리형 `PriorityQueue`
- `Jeomseon.Collections`의 일반 컬렉션 및 `IEnumerable<T>` 확장
- `Jeomseon.Reflection`의 멤버 Reflection 캐시, 런타임 타입 탐색 및 타입 생성

동일 소스에서 NuGet 패키지와 `com.jeomseon.core` UPM managed plug-in을 생성합니다.

## `Jeomseon.Unity.Core`

UnityEngine 타입과 실행 모델에 의존하는 어셈블리입니다. `package.json`에서
`com.jeomseon.core`를 의존하므로 Unity Package Manager가 managed plug-in을 자동 설치합니다.

- `Jeomseon.GameObjects`의 GameObject와 Transform 확장
- `Jeomseon.Mathematics`와 `Jeomseon.Events`의 Unity 값 타입 및 이벤트 확장
- `Jeomseon.UIElements`의 UI Toolkit 탐색
- `Jeomseon.Imaging`과 `Jeomseon.Rendering`의 CPU 픽셀 처리 및 Renderer Bounds 계산

순수 Core는 `Jeomseon.Core` 저장소, Unity 계층은 `Jeomseon.Unity.Core` 저장소에서 독립적으로
버전과 릴리스를 관리합니다.
