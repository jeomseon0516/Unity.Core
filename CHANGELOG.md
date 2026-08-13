# 변경 기록

## [0.3.0] - 2026-08-13

- **(Breaking)** rootNamespace를 `Jeomseon` → `Jeomseon.Unity.Core`로 변경하고, 하위 네임스페이스를
  전부 `Jeomseon.Unity.Core.<폴더>`로 정리했습니다(`Jeomseon.Events`→`Jeomseon.Unity.Core.Events`,
  `Jeomseon.GameObjects`→`Jeomseon.Unity.Core.GameObjects`,
  `Jeomseon.Imaging`→`Jeomseon.Unity.Core.Imaging`,
  `Jeomseon.Mathematics`→`Jeomseon.Unity.Core.Mathematics`,
  `Jeomseon.Rendering`→`Jeomseon.Unity.Core.Rendering`, `Jeomseon.Text`→`Jeomseon.Unity.Core.Text`,
  `Jeomseon.UIElements`→`Jeomseon.Unity.Core.UIElements`,
  `Jeomseon.Unity.Operations`(기존에도 유일하게 일관성이 어긋나 있었음)→`Jeomseon.Unity.Core.Operations`).
  워크스페이스 전체 네임스페이스 규칙(패키지 rootNamespace는 `Jeomseon.Unity.<패키지 폴더명>`, 하위는
  폴더 경로를 따름, `AGENTS.md` 참고)을 적용한 것으로, 폴더 구조 변경은 없습니다.

## [0.2.3] - 2026-08-11

- `Basic Usage` Sample에 `CoreCollectionsSample`이 이미 부착된 `CoreCollectionsSample.unity`
  Scene을 추가했습니다. 기존에는 Scene 자산 없이 README로 컨텍스트 메뉴 실행만 안내하고 있어
  `AGENTS.md`의 샘플 정책(Scene 자산 필수)을 충족하지 못했습니다.

## [0.2.2] - 2026-08-10

### Added

- Coroutine과 Awaitable 패키지가 공유할 Unity 작업 수명 계약 `IManagedOperation`과 `ManagedOperationStatus`를 추가했습니다.
- GameObject Pooling과 무관한 thread-safe `StringBuilderPool` 및 테스트를 Unity Core의 `Jeomseon.Text` 영역으로 이동했습니다.

## [0.2.0] - 2026-07-31

### Changed

- `object` 전체에 노출되던 필드 Reflection 확장을 제거하고, 캐시를 사용하는 `MemberReflection`의 명시적 API로 통합했습니다.
- `Deque<T>`를 노드 할당이 없는 원형 배열 기반 구현으로 교체했습니다.
- `PriorityQueue<T>`를 요소와 우선순위를 분리하고 최소·최대 힙을 선택할 수 있는 `PriorityQueue<TElement, TPriority>`로 재설계했습니다.
- 제네릭 `ForEach`를 유지하면서 비제네릭 overload를 제거하고, `ForEachSafe`를 `ForEachNotNull`, 컬렉션 fallback API를 `FallbackIfEmpty`로 명확하게 변경했습니다.
- Transform 축별 setter를 `TransformExtensions`로 통합하고 Component/GameObject의 단순 Transform Wrapper를 제거했습니다.
- RectTransform 좌표 Wrapper를 제거하고 호출부를 Unity `RectTransformUtility`로 이전했습니다.
- 표준 .NET·Unity API와 중복되는 Numeric 및 오브젝트 파괴 Helper를 제거했습니다.
- 텍스처 픽셀 리샘플링을 `TexturePixelResampler.ResizeToFit`으로 명확히 정의하고 입력 검증과 비정사각형 대상 영역 처리를 개선했습니다.
- Renderer 계층의 월드 경계 계산을 `RendererBoundsCalculator.TryCalculateWorldBounds`로 재설계했습니다.
- Unity 비의존 API를 `Jeomseon.Core` 어셈블리로 분리하고 Unity 계층의 참조 방향을 단방향으로 확정했습니다.
- 파괴적 Queue/Stack 열거를 `Drain`, 조건에 맞는 첫 List 요소 제거를 `RemoveFirst`로 명확하게 변경했습니다.
- 범용 `Jeomseon.Extensions`와 `Jeomseon.Helper` namespace를 제거하고 Collections, Reflection, GameObjects, Mathematics, Events, UIElements, Imaging 및 Rendering 책임으로 분리했습니다.
- 순수 `Jeomseon.Core`를 별도 저장소의 NuGet/UPM 이중 배포 패키지로 이동하고 `com.jeomseon.core` UPM 의존성으로 전환했습니다.

### Removed

- 코드로만 구성되는 상태 머신을 Core에서 제거했습니다. Inspector에서 설정 가능한 데이터 기반 상태 머신이 필요하면 별도 패키지로 설계합니다.
- Unity 6000.3.15f1에서 필요하지 않은 `IsExternalInit` 컴파일러 호환 어셈블리를 제거했습니다.
- 애플리케이션 도메인 정책인 `NicknameChecker`를 Core에서 제거했습니다.
- 표준 언어 및 .NET·Unity API와 중복되거나 계약이 불명확한 Class, Object, String, Dictionary, Parse, Delegate, Color, Rect 및 단순 수학 Helper를 제거했습니다.

## [0.1.3] - 2026-07-29

- asmdef의 `rootNamespace`와 소스 파일 위치를 namespace에 맞게 정리했습니다.
- `System.Runtime.CompilerServices` 호환 타입을 별도 어셈블리로 분리했습니다.

## [0.1.2] - 2026-07-29

- `Deque<T>` 사용법을 확인하는 `Basic Usage` 샘플을 추가했습니다.

이 패키지의 주요 변경 사항을 기록합니다.

## [0.1.1] - 2026-07-29

### Added

- Deque, PriorityQueue 및 StateMachine EditMode 단위 테스트를 추가했습니다.
- 누락됐던 ReflectionHelper와 EditMode 단위 테스트를 복구했습니다.
- 한국어 README와 영문 README를 분리했습니다.
- 패키지 구조와 버전 태그를 검사하는 GitHub Actions 워크플로를 추가했습니다.

## [0.1.0] - 2026-07-22

### Added

- JeomseonScriptPack에서 Core 모듈을 최초 분리했습니다.
- 컬렉션, 확장 메서드, 헬퍼, 직렬화 유틸리티 및 상태 머신을 추가했습니다.


## [0.2.1] - 2026-08-05

- Unity 6000.5.7f1을 최소 지원 버전으로 상향했습니다.
