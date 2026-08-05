# Core 로드맵

우선순위: `P0` 결함·안전성 → `P1` 핵심 구조 → `P2` API·성능 → `P3` 장기 확장

## 공통 판단 원칙

모든 모듈은 리팩토링 전에 다음 순서로 존치 필요성을 검토합니다.

1. 대상 Unity 버전이 지원하는 .NET API로 동일 기능을 대체할 수 있는지 확인합니다.
2. Unity 의존 기능은 Unity 기본 API 또는 Unity 공식 패키지로 대체할 수 있는지 확인합니다.
3. 표준 기능을 단순히 감싼 Wrapper라면 호출 편의성만으로 유지하지 않고 제거를 우선합니다.
4. 표준 기능으로 대체할 수 없거나 성능, 안정성, Inspector 작업 흐름, 직렬화 또는 도메인 특화 기능에서 명확한 이점이 있을 때만 자체 모듈을 유지합니다.
5. 자체 모듈을 유지할 경우 표준 기능과 비교한 존재 이유, 지원 범위 및 제한 사항을 문서화하고 테스트합니다.
6. Unity 의존성이 없는 기능은 순수 .NET 계층 및 별도 배포 후보로 분류합니다.

## 작업 순서

8. **P3-06 — 분리 배포 검증 체계 구축**
    - [x] Unity 참조 없이 순수 .NET 빌드와 단위 테스트가 통과하는 CI를 구성했습니다.
    - [x] 공개 OpenUPM 패키지만 사용하는 Unity 6000.5.7f1 프로젝트에서 컴파일, Mono Player 및 IL2CPP Player 빌드를 검증했습니다.
    - [x] IL2CPP Player 실행으로 캐시된 Reflection 필드 접근이 stripping 이후에도 동작하는지 검증했습니다.
    - [x] 순수 .NET DLL의 AssemblyRef와 Unity 패키지의 UPM 의존 방향·소스 중복을 검사하는 CI 경계 검사를 자동화했습니다.

## 향후 패키지 분리 후보

- `TexturePixelResampler`는 리샘플링·포맷 변환·압축 등 이미지 처리 기능군이 성장하면
  `Jeomseon.Unity.Imaging` 패키지로 분리합니다.
- `RendererBoundsCalculator`는 Mesh·Collider·Renderer의 공간 계산 기능군이 성장하면
  `Jeomseon.Unity.Geometry` 패키지로 분리합니다.
- 닉네임처럼 애플리케이션 정책에 속하는 검증 기능은 Core에 포함하지 않습니다.
  재사용 가능한 규칙 조합, 구조화된 검증 결과 및 Inspector 설정 기능이 필요해지면
  별도의 Validation 패키지로 설계합니다.
