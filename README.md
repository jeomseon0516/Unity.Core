# Jeomseon Unity Core

한국어 | [English](./README.en.md)

Jeomseon Unity 패키지들이 공통으로 사용하는 컬렉션, 확장 메서드, 헬퍼, 직렬화 유틸리티 및 상태 머신을 제공합니다.

## 요구 사항

- Unity 2022.3 이상

## OpenUPM으로 설치

프로젝트의 `Packages/manifest.json`에 OpenUPM scoped registry를 한 번 등록합니다.

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.jeomseon.unity"
      ]
    }
  ],
  "dependencies": {
    "com.jeomseon.unity.core": "0.1.1"
  }
}
```

## Git URL로 설치

Unity Package Manager의 `Install package from git URL`에 다음 URL을 입력합니다.

```text
https://github.com/jeomseon0516/Unity.Core.git#v0.1.2
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

- `Jeomseon.Collections`: Deque, PriorityQueue
- `Jeomseon.Extensions`: Unity 및 .NET 확장 메서드
- `Jeomseon.Helper`: 수학, 색상, 파싱, 문자열 및 텍스처 헬퍼
- `Jeomseon.Helper.ReflectionHelper`: 안전한 타입 검색 및 구현 타입 생성 유틸리티
- `Jeomseon.State`: 캐시 기반 상태 머신
- Unity 값 직렬화 유틸리티
- 컴파일러 호환 타입

## 제외된 기능

- Newtonsoft JSON에 의존하는 JsonUtility

선택적 외부 패키지를 Core에 강제하지 않도록 별도 모듈에서 제공합니다.

## 테스트

테스트 프로젝트의 manifest에 이 패키지를 `testables`로 등록한 후 Unity Test Runner의 EditMode에서 실행합니다.

## 호환성

기존 JeomseonScriptPack에서 옮긴 소스의 `.meta` GUID와 C# namespace를 유지하여 기존 Unity 에셋 참조와 소스 호환성을 보존합니다.

## 라이선스

[MIT License](./LICENSE.md)
