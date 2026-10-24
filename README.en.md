# Jeomseon Unity Core

[한국어](README.md) | English

Provides shared Unity Object/math/UI/imaging/text helpers and operation status contracts. Unity-independent collections live in Jeomseon.Core.

## Quick start

Call helpers in their feature namespaces. Pair StringBuilderPool Get/Release; IManagedOperation consumers observe Status, Exception, and Completed.

## Contracts and limitations

Respect Unity main-thread and resource lifetimes. StringBuilderPool thread safety does not extend to Unity helpers. Optional features such as JSON remain separate packages.

## Requirements

- Unity `6000.6.0f1`
- Package: `com.jeomseon.unity.core` `0.3.2`
- `com.jeomseon.core`: `0.1.3`
- `com.unity.modules.particlesystem`: `1.0.0`
- `com.unity.modules.uielements`: `1.0.0`

## Installation

Merge this into your project's `Packages/manifest.json`; preserve existing dependencies and registries.

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

Or use **Install package from git URL** in Package Manager:

```text
https://github.com/jeomseon0516/Unity.Core.git#v0.3.2
```

## API usage and details

### Contents

- `Jeomseon.Unity.Core.GameObjects`: GameObject and Transform extensions
- `Jeomseon.Unity.Core.Mathematics`: Unity Color and Vector conversions and operations
- `Jeomseon.Unity.Core.Events`, `Jeomseon.Unity.Core.UIElements`: UnityEvent and UI Toolkit extensions
- `Jeomseon.Unity.Core.Text`: thread-safe `StringBuilderPool` with retention limits and duplicate-return validation
- `Jeomseon.Unity.Core.Operations`: Unity operation lifetime contracts shared by Coroutine and Awaitable implementations
- `Jeomseon.Unity.Core.Imaging`, `Jeomseon.Unity.Core.Rendering`: CPU pixel resampling and Renderer bounds calculation

### Dependency package

- UPM installs `com.jeomseon.core` alongside this package.
- `Jeomseon.Collections`: Deque, PriorityQueue, and general collection extensions
- `Jeomseon.Reflection`: type discovery, instance activation, and cached member access

### Excluded integrations

- JsonUtility, which depends on Newtonsoft JSON

These integrations remain outside Core to avoid imposing unrelated dependencies.

### Testing

Add this package to the integration project's `testables` list, then run the EditMode suite with Unity Test Runner.

## Samples

Import the desired entry from **Package Manager > Samples**. Use the included Scene/Editor tool and complete its Setup step first, if required.

- [Basic Usage](Samples~/BasicUsage/README.md)
  - [CoreCollectionsSample](Samples~/BasicUsage/CoreCollectionsSample.unity)

## Tests

[Test sources](Tests)

Add the package ID to manifest `testables`, then execute its relevant EditMode/PlayMode assemblies in Unity Test Runner.

## Further reading

[Complete source map and package metadata](Documentation~/Overview.en.md)
- [Changelog](CHANGELOG.md)

## License

[License](LICENSE.md)
