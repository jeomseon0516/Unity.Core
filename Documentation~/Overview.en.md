# Jeomseon Unity Core

[한국어](Overview.md) · [Installation](../README.en.md)

## Purpose

Provides shared Unity Object/math/UI/imaging/text helpers and operation status contracts. Unity-independent collections live in Jeomseon.Core.

## Usage flow

Call helpers in their feature namespaces. Pair StringBuilderPool Get/Release; IManagedOperation consumers observe Status, Exception, and Completed.

## Contracts and limitations

Respect Unity main-thread and resource lifetimes. StringBuilderPool thread safety does not extend to Unity helpers. Optional features such as JSON remain separate packages.

## Package requirements

- Package ID: `com.jeomseon.unity.core`
- Version: `0.3.2`
- Unity: `6000.6` / `0f1`
- `com.jeomseon.core`: `0.1.3`
- `com.unity.modules.particlesystem`: `1.0.0`
- `com.unity.modules.uielements`: `1.0.0`

## Samples and verification

Import the following from Package Manager > Samples. Open the included Scene or Editor tool; follow any Setup step in its sample README.

- [Basic Usage](../Samples~/BasicUsage/README.md)
  - [CoreCollectionsSample](../Samples~/BasicUsage/CoreCollectionsSample.unity)

[Test sources](../Tests)

Add the package ID to manifest `testables`, then run the relevant Unity Test Runner assemblies. These links describe available tests, not a new successful test run.

## Source map

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
