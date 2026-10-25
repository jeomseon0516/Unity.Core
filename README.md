# Jeomseon Unity Core

한국어 | [English](README.en.md)

Unity Object·수학·UI·이미지·문자열 관련 공통 확장과 작업 상태 계약을 제공합니다. Unity 비의존 자료구조는 Jeomseon.Core에 둡니다.

## 빠르게 시작하기

필요한 namespace의 확장 메서드를 직접 호출합니다. StringBuilderPool을 사용하면 Get/Release를 대칭으로 하고, IManagedOperation 소비자는 Status·Exception·Completed 계약을 사용합니다.

## 계약과 주의사항

Unity Object나 렌더링 API는 메인 스레드와 리소스 수명을 지켜야 합니다. StringBuilderPool의 thread safety가 다른 Unity API까지 보장하지 않습니다. JSON 같은 선택적 기능은 별도 패키지입니다.

## 요구 사항

- Unity `6000.6.0f1`
- 패키지: `com.jeomseon.unity.core` `0.3.2`
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
    "com.jeomseon.unity.core": "0.3.2"
  }
}
```

또는 Package Manager의 **Install package from git URL**을 사용합니다:

```text
https://github.com/jeomseon0516/Unity.Core.git#v0.3.2
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
