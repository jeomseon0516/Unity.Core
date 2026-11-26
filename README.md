# Jeomseon Unity Core

한국어 | [English](README.en.md)

Unity Object·수학·UI·이미지·문자열 관련 공통 확장과 작업 상태 계약을 제공합니다. Unity 비의존 자료구조는 Jeomseon.Core에 둡니다.

## 카메라 뷰포트와 그리드 셀 범위

- `Jeomseon.Unity.Core.Rendering.ViewportPlaneProjection.TryProjectCorners`는 정규화된 뷰포트의 네 모서리를 월드 평면에 투영합니다. 호출자가 준비한 `Span<Vector3>`의 첫 네 칸에 좌하단, 우하단, 좌상단, 우상단 순으로 기록합니다. 광선 하나라도 전방에서 유한한 교점을 얻지 못하면 `false`를 반환하고 버퍼를 유지합니다.
- `Jeomseon.Unity.Core.Grids.ViewportCellBounds.TryCalculate`는 그리드 로컬 XY 평면에 투영해 셀 축에 정렬된 `BoundsInt`를 반환합니다. 직사각형 레이아웃과 XYZ 셀 축 순서를 지원합니다. X와 Y는 최소값을 내리고 최대값을 올린 반개구간이고, Z는 `[0, 1)`입니다. 투영에 실패하거나 레이아웃을 지원하지 않거나 셀 범위를 `BoundsInt`로 표현할 수 없으면 `false`와 기본 경계를 반환합니다.

광선은 카메라의 근거리 클리핑 평면에서 시작합니다. 두 API는 투영 면적을 계산하며 원거리 클리핑과 가림 판정을 적용하지 않습니다. 카메라나 그리드가 없거나 파괴됐으면 `ArgumentNullException`을, 모서리 버퍼가 네 칸 미만이면 `ArgumentException`을 던집니다.

## 빠르게 시작하기

필요한 namespace의 확장 메서드를 직접 호출합니다. StringBuilderPool을 사용하면 Get/Release를 대칭으로 하고, IManagedOperation 소비자는 Status·Exception·Completed 계약을 사용합니다.

## 계약과 주의사항

Unity Object나 렌더링 API는 메인 스레드와 리소스 수명을 지켜야 합니다. StringBuilderPool의 thread safety가 다른 Unity API까지 보장하지 않습니다. JSON 같은 선택적 기능은 별도 패키지입니다.

## 요구 사항

- Unity `6000.6.0f1`
- 패키지: `com.jeomseon.unity.core` `0.4.0`
- `com.jeomseon.core`: `0.1.3`
- `com.unity.modules.particlesystem`: `1.0.0`
- `com.unity.modules.uielements`: `1.0.0`

## 설치

프로젝트 `Packages/manifest.json`의 기존 dependencies와 registry를 유지하면서 다음 항목을 병합합니다.

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.jeomseon"
      ]
    }
  ],
  "dependencies": {
    "com.jeomseon.unity.core": "0.4.0"
  }
}
```

또는 Package Manager의 **Install package from git URL**을 사용합니다:

```text
https://github.com/jeomseon0516/Unity.Core.git#v0.4.0
```

## API 사용과 상세 설명

### 포함 기능

- `Jeomseon.Unity.Core.GameObjects`: GameObject와 Transform 확장
- `Jeomseon.Unity.Core.Mathematics`: Unity Color와 Vector 변환 및 연산
- `Jeomseon.Unity.Core.Events`, `Jeomseon.Unity.Core.UIElements`: UnityEvent와 UI Toolkit 확장
- `Jeomseon.Unity.Core.Text`: 용량 제한과 중복 반환 검사를 제공하는 thread-safe `StringBuilderPool`
- `Jeomseon.Unity.Core.Operations`: Coroutine·Awaitable 구현이 공유하는 Unity 작업 수명 계약
- `Jeomseon.Unity.Core.Imaging`, `Jeomseon.Unity.Core.Rendering`: CPU 픽셀 리샘플링과 Renderer Bounds 계산

### 의존 패키지

- UPM이 `com.jeomseon.core`를 함께 설치합니다.
- `Jeomseon.Collections`: Deque, PriorityQueue 및 일반 컬렉션 확장
- `Jeomseon.Reflection`: 타입 탐색, 인스턴스 생성 및 캐시된 멤버 접근

### 제외된 기능

- Newtonsoft JSON에 의존하는 JsonUtility

선택적 외부 패키지를 Core에 강제하지 않도록 별도 모듈에서 제공합니다.

## 샘플 실행

**Package Manager > Samples**에서 예제를 Import합니다. 포함된 Scene 또는 Editor 도구를 열고 필요한 Setup 메뉴를 먼저 실행합니다.

- [Basic Usage](Samples~/BasicUsage/README.md)
  - [CoreCollectionsSample](Samples~/BasicUsage/CoreCollectionsSample.unity)

## 테스트

[테스트 코드](Tests)

manifest의 `testables`에 패키지 ID를 추가하고 Unity Test Runner의 관련 EditMode/PlayMode 어셈블리를 실행합니다.

## 추가 문서

[전체 코드 지도와 패키지 구성](Documentation~/Overview.md)
- [Changelog](CHANGELOG.md)

## 라이선스

[License](LICENSE.md)
