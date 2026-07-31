# Jeomseon Unity Core

[한국어](./README.md) | English

Jeomseon Unity Core provides Unity-specific extensions and feature modules shared by Jeomseon Unity packages.

## Requirements

- Unity 6000.3.15f1 or newer

## Install with OpenUPM

Register the OpenUPM scoped registry once in `Packages/manifest.json`.

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

## Install from Git

Use this URL with Unity Package Manager's `Install package from git URL` command.

```text
https://github.com/jeomseon0516/Unity.Core.git#v0.2.0
```

## Local development

Reference this repository as a local package from an integration project.

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

The package remains editable under `Packages/Jeomseon Unity Core` in the Unity Project window.

## Contents

- `Jeomseon.GameObjects`: GameObject and Transform extensions
- `Jeomseon.Mathematics`: Unity Color and Vector conversions and operations
- `Jeomseon.Events`, `Jeomseon.UIElements`: UnityEvent and UI Toolkit extensions
- `Jeomseon.Imaging`, `Jeomseon.Rendering`: CPU pixel resampling and Renderer bounds calculation

## Dependency package

- UPM installs `com.jeomseon.core` alongside this package.
- `Jeomseon.Collections`: Deque, PriorityQueue, and general collection extensions
- `Jeomseon.Reflection`: type discovery, instance activation, and cached member access

## Excluded integrations

- JsonUtility, which depends on Newtonsoft JSON

These integrations remain outside Core to avoid imposing unrelated dependencies.

## Testing

Add this package to the integration project's `testables` list, then run the EditMode suite with Unity Test Runner.

## License

[MIT License](./LICENSE.md)
