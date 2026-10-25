# Jeomseon Unity Core

[English](Overview.en.md) · [설치](../README.md)

## 목적

Unity Object·수학·UI·이미지·문자열 관련 공통 확장과 작업 상태 계약을 제공합니다. Unity 비의존 자료구조는 Jeomseon.Core에 둡니다.

## 사용 흐름

필요한 namespace의 확장 메서드를 직접 호출합니다. StringBuilderPool을 사용하면 Get/Release를 대칭으로 하고, IManagedOperation 소비자는 Status·Exception·Completed 계약을 사용합니다.

## 계약과 제한

Unity Object나 렌더링 API는 메인 스레드와 리소스 수명을 지켜야 합니다. StringBuilderPool의 thread safety가 다른 Unity API까지 보장하지 않습니다. JSON 같은 선택적 기능은 별도 패키지입니다.

## 패키지 요구 사항

- Package ID: `com.jeomseon.unity.core`
- Version: `0.3.2`
- Unity: `6000.6` / `0f1`
- `com.jeomseon.core`: `0.1.3`
- `com.unity.modules.particlesystem`: `1.0.0`
- `com.unity.modules.uielements`: `1.0.0`

## 샘플과 검증

Package Manager > Samples에서 다음 예제를 Import하세요. 포함된 Scene 또는 Editor 도구를 열고 샘플 README의 Setup 절차를 먼저 수행합니다.

- [Basic Usage](../Samples~/BasicUsage/README.md)
  - [CoreCollectionsSample](../Samples~/BasicUsage/CoreCollectionsSample.unity)

[테스트 코드](../Tests)

소비 프로젝트 manifest의 `testables`에 패키지 ID를 추가하고 Unity Test Runner의 관련 어셈블리를 실행합니다. 위 링크는 테스트 위치이며 이번 문서 수정으로 새 테스트 실행을 주장하지 않습니다.

## 코드 지도

- [UnityEventExtensions](../Runtime/Events/UnityEventExtensions.cs)
- [GameObjectExtensions](../Runtime/GameObjects/GameObjectExtensions.cs)
- [TransformExtensions](../Runtime/GameObjects/TransformExtensions.cs)
- [TexturePixelResampler](../Runtime/Imaging/TexturePixelResampler.cs)
- [ColorExtensions](../Runtime/Mathematics/ColorExtensions.cs)
- [Vector2Extensions](../Runtime/Mathematics/Vector2Extensions.cs)
- [Vector2IntExtensions](../Runtime/Mathematics/Vector2IntExtensions.cs)
- [Vector3Extensions](../Runtime/Mathematics/Vector3Extensions.cs)
- [Vector3IntExtensions](../Runtime/Mathematics/Vector3IntExtensions.cs)
- [IManagedOperation](../Runtime/Operations/IManagedOperation.cs)
- [ManagedOperationStatus](../Runtime/Operations/ManagedOperationStatus.cs)
- [RendererBoundsCalculator](../Runtime/Rendering/RendererBoundsCalculator.cs)
- [StringBuilderPool](../Runtime/Text/StringBuilderPool.cs)
- [VisualElementExtensions](../Runtime/UIElements/VisualElementExtensions.cs)
