# Jeomseon Unity Core

한국어 | [English](./README.en.md)

Jeomseon Unity 패키지들이 공통으로 사용하는 Unity 전용 확장과 기능 모듈을 제공합니다.

## 요구 사항

- Unity 6000.3.15f1 이상

## OpenUPM으로 설치

프로젝트의 `Packages/manifest.json`에 OpenUPM scoped registry를 한 번 등록합니다.

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
    "com.jeomseon.unity.core": "0.2.0"
  }
}
```

## Git URL로 설치

Unity Package Manager의 `Install package from git URL`에 다음 URL을 입력합니다.

```text
https://github.com/jeomseon0516/Unity.Core.git#v0.2.0
```

## 로컬 개발

통합 테스트 프로젝트에서 이 저장소를 로컬 패키지로 연결합니다.

```json
{
  "dependencies": {
    "com.jeomseon.unity.core": "file:../../Jeomseon.Unity.Core"
  },
  "testables": [
    "com.jeomseon.unity.core"
  ]
}
```

Unity Project 창의 `Packages/Jeomseon Unity Core`에서 코드를 확인하고 수정할 수 있습니다.

## 포함 기능

- `Jeomseon.GameObjects`: GameObject와 Transform 확장
- `Jeomseon.Mathematics`: Unity Color와 Vector 변환 및 연산
- `Jeomseon.Events`, `Jeomseon.UIElements`: UnityEvent와 UI Toolkit 확장
- `Jeomseon.Imaging`, `Jeomseon.Rendering`: CPU 픽셀 리샘플링과 Renderer Bounds 계산

## 의존 패키지

- UPM이 `com.jeomseon.core`를 함께 설치합니다.
- `Jeomseon.Collections`: Deque, PriorityQueue 및 일반 컬렉션 확장
- `Jeomseon.Reflection`: 타입 탐색, 인스턴스 생성 및 캐시된 멤버 접근

## 제외된 기능

- Newtonsoft JSON에 의존하는 JsonUtility

선택적 외부 패키지를 Core에 강제하지 않도록 별도 모듈에서 제공합니다.

## 테스트

테스트 프로젝트의 manifest에 이 패키지를 `testables`로 등록한 후 Unity Test Runner의 EditMode에서 실행합니다.

## 라이선스

[MIT License](./LICENSE.md)
